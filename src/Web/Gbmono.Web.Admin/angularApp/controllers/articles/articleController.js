/*
    article list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$filter',
                    'pluginService',
                    'articleDataFactory'];

    // create controller
    module.controller('articleListController', ctrl);

    // controller body
    function ctrl($scope,
                  $filter,
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

        // open create window
        $scope.openCreateWin = function () {
            $scope.winCreate.open();
        };

        // create new article
        $scope.create = function () {
            createArticle($scope.newArticle);
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
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 100
                    }
                ]
            };
        }

        function createArticle(article) {
            articleDataFactory.create(article)
                .success(function (data) {
                    console.log(data);
                })
                .error(function (error) {
                    pluginService.notify('error', error);
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
                    'articleDataFactory'];

    // create controller
    module.controller('articleEditController', ctrl);

    // controller body
    function ctrl($scope,
                  $routeParams,
                  utilService,
                  articleDataFactory) {
        // retreive id from url
        var articleId = $routeParams.articleId ? parseInt($routeParams.articleId) : 0;

        // retreive token from local storage
        var token = utilService.getToken();

        // page init
        init();

        function init() {
            initKendoEditor();
        }

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
                        "fontName",
                        "fontSize",
                        "print"
                    ],
                    imageBrowser: {
                        messages: {
                            dropFilesHere: "Drop files here"
                        },
                        transport: {
                            // read: gbmono.api_site_prefix.article_api_url + "/BrowseImages",
                            //destroy: {
                            //    url: metsys.api_file_prefix + "/DeleteImage",
                            //    type: "POST"
                            //},
                            read: {
                                url: function () {                     
                                    return gbmono.api_site_prefix.article_api_url + "/BrowseImages/" + articleId;
                                },
                                headers: { Authorization: 'Bearer ' + token } // bear token
                            },

                            thumbnailUrl: gbmono.api_site_prefix.article_api_url + "/Thumbnails/{0}/" + articleId,
                            //thumbnailUrl: {
                            //    url: function(){
                            //        return gbmono.api_site_prefix.article_api_url + "/Thumbnails/{0}/" + articleId;
                            //    },
                            //    headers: { Authorization: 'Bearer ' + token }
                            //},
                            uploadUrl: gbmono.api_site_prefix.article_api_url + "/Upload",
                            imageUrl: gbmono.img_article_path + '/' + articleId + '/{0}'
                            // imageUrl: gbmono.api_site_prefix.article_api_url + "/GetImage?path={0}"
                        }
                    }
                }
            );

            // var editor = $("#page-content").data("kendoEditor");

            // set value for kendo editor with page content
            // editor.value($scope.editPage.pageContent);
        }
    }
})(angular.module('gbmono'));