/*
    article list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$filter',
                    '$location',
                    'pluginService',
                    'articleDataFactory'];

    // create controller
    module.controller('articleListController', ctrl);

    // controller body
    function ctrl($scope,
                  $filter,
                  $location,
                  pluginService,
                  articleDataFactory) {
        // new article
        $scope.newArticle = { articleTypeId : 1}; // default  selection
        // article types
        $scope.articleTypes = [{ name: "News", value: 1 }, { name: "Blog", value: 2 }]
        // filter
        $scope.filter = {
            from: $filter('date')(new Date().setDate(new Date().getDate() - 7), 'yyyy-MM-dd'),
            to: $filter('date')(new Date(), 'yyyy-MM-dd'),
            type: 1
        };

        // page init
        init();

        // reload data
        $scope.reload = function () {
            $scope.grid.dataSource.read();
        };

        // open create window
        $scope.openCreateWin = function () {
            $scope.winCreate.open();
        };

        // create new article
        $scope.create = function () {
            createArticle($scope.newArticle);
        };

        // delete article
        $scope.delete = function (id) {
            var r = confirm('Are you sure to delete? ');
            if (r) {
                deleteArticle(id);
            }
        };

        function init() {
            bindArticleGrid();
        }

        // retreive brand data and binding it into kendo grid
        function bindArticleGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            articleDataFactory.getByDate($scope.filter.from, $scope.filter.to, $scope.filter.type)
                                .success(function (data) {
                                    // call back
                                    e.success(data);
                                });
                        }
                    }
                },
                pageable: {
                    numeric: false,
                    previousNext: false,
                    messages: {
                        display: "总计: {2}"
                    }
                },
                sortable: true,
                height: 650,
                filterable: false,
                columns: [
                    { field: "title", title: "标题" },
                    {
                        field: "createdDate", title: "创建日期", width: 100,
                        template: "#= kendo.toString(kendo.parseDate(createdDate), 'yyyy-MM-dd') #"
                    },
                    { field: "createdBy", title: "创建用户", width: 150 },
                    {
                        field: "modifiedDate", title: "修改日期", width: 100,
                        template: "#= kendo.toString(kendo.parseDate(modifiedDate), 'yyyy-MM-dd') #"
                    },
                    { field: "modifiedBy", title: "修改用户", width: 150 },
                    {
                        template: '<a class="btn btn-xs btn-info" ng-href="\\#/articles/edit/#=articleId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click="delete(dataItem.articleId)"><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 100
                    }
                ]
            };
        }

        // create new article with title only
        function createArticle(article) {
            articleDataFactory.create(article)
                .success(function (data) {
                    // redirect into edit page
                    $location.path('/articles/edit/' + data);
                })
                .error(function (error) {
                    pluginService.notify('error', error);
                });
        }

        // delete article
        function deleteArticle(id) {
            articleDataFactory.del(id)
                .success(function () {
                    // reload data
                    $scope.grid.dataSource.read();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
    }
})(angular.module('gbmono'));

/*
    article edit controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'utilService',
                    'pluginService',
                    'articleDataFactory',
                    'tagDataFactory'];

    // create controller
    module.controller('articleEditController', ctrl);

    // controller body
    function ctrl($scope,
                  $routeParams,
                  utilService,
                  pluginService,
                  articleDataFactory,
                  tagDataFactory) {
        // retreive id from url
        var articleId = $routeParams.articleId ? parseInt($routeParams.articleId) : 0;

        // edit model
        $scope.editArticle = {};

        // kendo multiple selection options
        $scope.selectOptions = {};

        // selected product tag ids
        $scope.selectedTagIds = [];

        // retreive token from local storage
        var token = utilService.getToken();

        // page init
        init();

        // event handlers
        $scope.update = function () {
            // read content from kendo editor
            var content = $('#content').val();

            // convert into html
            var html = $('#content').html(content).text();
            
            // update html content
            $scope.editArticle.content = html;
            
            // update
            save($scope.editArticle);
        };

        function init() {
            // kendo multi-selection
            bindTagSelection();

            // load article
            getArticleById(articleId);
        }

        // init kendo editor
        function initKendoEditor() {
            $("#content").kendoEditor(
                {
                    tools: [
                        "bold",
                        "italic",
                        "underline",
                        "strikethrough",
                        "justifyLeft",
                        "justifyCenter",
                        "justifyRight",
                        "justifyFull",
                        "insertUnorderedList",
                        "insertOrderedList",
                        "indent",
                        "outdent",
                        "createLink",
                        "unlink",
                        "insertImage",
                        "createTable",
                        "addRowAbove",
                        "addRowBelow",
                        "addColumnLeft",
                        "addColumnRight",
                        "deleteRow",
                        "deleteColumn",
                        "viewHtml",
                        "formatting",
                        "cleanFormatting",
                        //"fontName",
                        //"fontSize",
                        "print"
                    ],
                    imageBrowser: {
                        messages: {
                            dropFilesHere: "Drop files here"
                        },
                        transport: {
                            //destroy: {
                            //    url: gbmono.api_site_prefix.article_api_url + "/DeleteImage",
                            //    type: "POST"
                            //},
                            read: { // image list browse
                                url: function () {                     
                                    return gbmono.api_site_prefix.article_api_url + "/BrowseImages/" + articleId;
                                },
                                headers: { Authorization: 'Bearer ' + token } // bear token
                            },
                            thumbnailUrl: function (path, name) { // each single thubnail
                                return gbmono.img_article_path + articleId + '/' + 'thumbnails' + '/' + name;
                            },                            
                            uploadUrl: gbmono.api_site_prefix.article_api_url + "/Upload/" + articleId, // upload image action
                            imageUrl: gbmono.img_article_path +  articleId + '/{0}'  // selected image url                           
                        }
                    },
                    execute: function (e) {
                        // attach bearer token into request when uploading image
                        if (e.name == "insertimage") {
                            setTimeout(function () {
                                var imagebrowser = $("[data-role=imagebrowser]").data("kendoImageBrowser");
                                imagebrowser.upload.bind("upload", function (e) {
                                    var xhr = e.XMLHttpRequest;
                                    if (xhr) {
                                        xhr.addEventListener("readystatechange", function (e) {
                                            if (xhr.readyState === 1 /* OPENED */) {
                                                xhr.setRequestHeader("Authorization", "Bearer " + token);
                                            }
                                        });
                                    }
                                });
                            }, 0);
                        }
                    }
                }
            );

            var editor = $("#content").data("kendoEditor");

            // set value for kendo editor with page content
            editor.value($scope.editArticle.content);
        }

        // bind article tags into multi-selection
        function bindTagSelection() {
            $scope.selectOptions = {
                placeholder: "Select tags...",
                dataTextField: "name",
                dataValueField: "tagId",
                valuePrimitive: true,
                autoBind: false,
                dataSource: {
                    transport: {
                        read: function (e) {
                            tagDataFactory.getByType(2) // retreive product tags
                                .success(function (data) {
                                    // kendo selection callback
                                    e.success(data);
                                });
                        }
                    }
                }
            };
        }

        // get article by id
        function getArticleById(id) {
            articleDataFactory.getById(id)
                .success(function (data) {
                    // retreive the  data
                    $scope.editArticle = data;

                    // init kendo ui editor
                    // retreive html content 
                    initKendoEditor();
                });
        }

        // save article
        function save(model) {
            articleDataFactory.update(model)
                .success(function () {
                    pluginService.notify('更新成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
    }
})(angular.module('gbmono'));