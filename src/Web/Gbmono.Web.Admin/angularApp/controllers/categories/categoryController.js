/*
    top category list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'categoryDataFactory'];

    // create controller
    module.controller('topCategoryController', ctrl);

    // controller body
    function ctrl($scope, categoryDataFactory) {

        // page init
        init();

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
                        template: '<button class="btn btn-xs btn-info" ng-click=""><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
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
                    'categoryDataFactory'];

    // create controller
    module.controller('secondCategoryController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, categoryDataFactory) {
        // retreive parent id from route
        var parentId = $routeParams.parentId ? parseInt($routeParams.parentId) : 0;
        // parent category
        $scope.parentCategory = {};

        // page init
        init();

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
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/categories/#=categoryId#/third">#= name #</a>',
                    },
                    {
                        template: '<button class="btn btn-xs btn-info" ng-click=""><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
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

        // page init
        init();

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
                        template: '<button class="btn btn-xs btn-info" ng-click=""><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
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
    }
})(angular.module('gbmono'));