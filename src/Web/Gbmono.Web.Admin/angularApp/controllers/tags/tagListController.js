/*
    tag list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'pluginService',
                    'tagDataFactory'];

    // create controller
    module.controller('tagListController', ctrl);

    // controller body
    function ctrl($scope, pluginService, tagDataFactory) {
        // new tag
        $scope.newTag = {};
        // edit tag
        $scope.editTag = {};

        // page init
        init();

        // event hadlers
        // show create tag window
        $scope.showCreate = function () {
            // reset newtag
            $scope.newTag = {};

            // open window
            $scope.winCreate.open();
        };

        // create new tag
        $scope.create = function () {
            createTag($scope.newTag);
        };

        // show edit tag window
        $scope.showEdit = function (dataItem) {
            $scope.editTag.tagId = dataItem.tagId;
            $scope.editTag.name = dataItem.name;

            // open window
            $scope.winEdit.open();
        };

        // update tag
        $scope.update = function () {
            updateTag($scope.editTag);
        };

        // delete tag
        $scope.delete = function (id) {
            var r = confirm('Are you sure to delete? ');
            if (r) {
                deleteTag(id);
            }
        };

        function init() {
            // load all tags
            bindTagGrid();
        }

        // retreive brand data and binding it into kendo grid
        function bindTagGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            tagDataFactory.getAll()
                                .success(function (data) {
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
                    { field: "name", title: "名称", width: 120 },
                    {
                        template: '<button class="btn btn-xs btn-info" ng-click="showEdit(dataItem)"><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click="delete(dataItem.tagId)"><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
        }

        // create tag
        function createTag(tag) {
            tagDataFactory.create(tag)
                .success(function (data) {
                    // reload data
                    $scope.grid.dataSource.read();
                    // close window
                    $scope.winCreate.close();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }

        // update tag
        function updateTag(tag) {
            tagDataFactory.update(tag)
                .success(function (data) {
                    // reload data
                    $scope.grid.dataSource.read();
                    // close window
                    $scope.winEdit.close();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }

        // delete tag
        function deleteTag(id) {
            tagDataFactory.del(id)
                .success(function () {
                    // reload data
                    $scope.grid.dataSource.read();
                    // close window
                    $scope.winEdit.close();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
    }
})(angular.module('gbmono'));