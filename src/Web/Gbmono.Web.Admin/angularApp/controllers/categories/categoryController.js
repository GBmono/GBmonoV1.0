﻿/*
    top category list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'pluginService',
                    'categoryDataFactory'];

    // create controller
    module.controller('topCategoryController', ctrl);

    // controller body
    function ctrl($scope, pluginService, categoryDataFactory) {
        // edit category
        $scope.editCategory = {};

        // page init
        init();

        // event hadlers
        $scope.showEdit = function (dataItem) {
            $scope.editCategory.categoryId = dataItem.categoryId;
            $scope.editCategory.parentId = dataItem.parentId;
            $scope.editCategory.categoryCode = dataItem.categoryCode;
            $scope.editCategory.name = dataItem.name;
            // show window
            $scope.winEdit.open();
        };
        
        // update event handelr
        $scope.update = function () {
            updateCategory($scope.editCategory);
        };

        function init() {
            bindCategoryGrid();
        }

        // retreive brand data and binding it into kendo grid
        function bindCategoryGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            categoryDataFactory.getTopCategories()
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
                    { field: "categoryCode", title: "代码", width: 120 },
                    {
                        field: "name", title: "名称",
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/categories/#=categoryId#/second">#= name #</a>',
                    },
                    {
                        template: '<button class="btn btn-xs btn-info" ng-click="showEdit(dataItem)"><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
        }

        // update category
        function updateCategory(category) {
            categoryDataFactory.update(category)
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
    }
})(angular.module('gbmono'));

/*
    second category list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'pluginService',
                    'categoryDataFactory'];

    // create controller
    module.controller('secondCategoryController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, categoryDataFactory) {
        // retreive parent id from route
        var parentId = $routeParams.parentId ? parseInt($routeParams.parentId) : 0;
        // parent category
        $scope.parentCategory = {};
        // top categories
        $scope.topCategories = [];
        // edit category
        $scope.editCategory = {};

        // page init
        init();

        // event hadlers
        $scope.showEdit = function (dataItem) {
            $scope.editCategory.categoryId = dataItem.categoryId;
            $scope.editCategory.parentId = dataItem.parentId;
            $scope.editCategory.categoryCode = dataItem.categoryCode;
            $scope.editCategory.name = dataItem.name;
            // show window
            $scope.winEdit.open();
        };

        // update event handelr
        $scope.update = function () {
            updateCategory($scope.editCategory);
        };

        function init() {
            // get parent category
            getCategory(parentId);

            // get second categories
            bindCategoryGrid();

            // get top categories in the edit form
            getTopCategories();
        }

        // retreive brand data and binding it into kendo grid
        function bindCategoryGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            categoryDataFactory.getByParent(parentId)
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
                    { field: "categoryCode", title: "代码", width: 120 },
                    {
                        field: "name", title: "名称",
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/categories/#=categoryId#/third">#= name #</a>',
                    },
                    {
                        template: '<button class="btn btn-xs btn-info" ng-click="showEdit(dataItem)"><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
        }

        // get parent category
        function getCategory(parentId) {
            categoryDataFactory.getById(parentId)
                .success(function (data) {
                    $scope.parentCategory = data;
                });
        }

        // get top categories
        function getTopCategories() {
            categoryDataFactory.getTopCategories()
                .success(function (data) {
                    $scope.topCategories = data;
                });
        }

        // update category
        function updateCategory(category) {
            categoryDataFactory.update(category)
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
    }
})(angular.module('gbmono'));

/*
    third category list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'categoryDataFactory'];

    // create controller
    module.controller('thirdCategoryController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, categoryDataFactory) {
        // retreive parent id from route
        var parentId = $routeParams.parentId ? parseInt($routeParams.parentId) : 0;
        // parent category
        $scope.parentCategory = {};
        // top categories
        $scope.topCategories = [];
        $scope.selectedTopCateId = 0;
        // second
        $scope.secondCategories = [];

        // edit category
        $scope.editCategory = {};

        // page init
        init();

        // event hadlers
        $scope.showEdit = function (dataItem) {
            $scope.editCategory.categoryId = dataItem.categoryId;
            $scope.editCategory.parentId = dataItem.parentId;
            $scope.editCategory.categoryCode = dataItem.categoryCode;
            $scope.editCategory.name = dataItem.name;
            // show window
            $scope.winEdit.open();
        };

        // top cate selection changed
        $scope.topCateChanged = function () {
            getSecondCategories($scope.selectedTopCateId);
        };

        // update event handelr
        $scope.update = function () {
            updateCategory($scope.editCategory);
        };

        function init() {
            // get parent category
            getCategory(parentId);

            // get second categories
            bindCategoryGrid();
        }

        // retreive brand data and binding it into kendo grid
        function bindCategoryGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            categoryDataFactory.getByParent(parentId)
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
                    { field: "categoryCode", title: "代码", width: 120 },
                    {
                        field: "name", title: "名称",
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/categories/#=categoryId#/products">#= name #</a>',
                    },
                    {
                        template: '<button class="btn btn-xs btn-info" ng-click="showEdit(dataItem)"><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
        }

        // get parent category
        function getCategory(parentId) {
            categoryDataFactory.getById(parentId)
                .success(function (data) {
                    $scope.parentCategory = data;
                    // load second category list by top cate id
                    getSecondCategories($scope.parentCategory.parentId);
                    // load all top categories
                    getTopCategories($scope.parentCategory.parentId);
                });
        }

        // get top categories
        function getTopCategories(selectedId) {
            categoryDataFactory.getTopCategories()
                .success(function (data) {
                    $scope.topCategories = data;
                    // select top cate id
                    $scope.selectedTopCateId = selectedId
                });
        }
        
        // get second categories
        function getSecondCategories(topCateId) {
            categoryDataFactory.getByParent(topCateId)
                .success(function (data) {
                    $scope.secondCategories = data;
                })
        }

        // update category
        function updateCategory(category) {
            categoryDataFactory.update(category)
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
    }
})(angular.module('gbmono'));