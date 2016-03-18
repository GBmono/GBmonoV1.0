/*
    product browse by category page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'categoryDataFactory',
                    'productDataFactory'];

    // create controller
    module.controller('productBrowseController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, categoryDataFactory, productDataFactory) {
        // current category model
        $scope.category = {};

        // kendo ui grid binding options
        $scope.mainGridOptions = {}

        // retreive category from route
        $scope.categoryId = $routeParams.id ? parseInt($routeParams.id) : 0;

        // page init
        init();

        // reload data
        $scope.reload = function () {
            // parent grid
            $scope.grid.dataSource.read();
        }

        function init() {
            getCategory($scope.categoryId);

            // bind data
            bindProductGrid();
        }

        // retreive product data and binding it into kendo grid
        function bindProductGrid() {
            // init kendo ui grid with location data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            productDataFactory.getByCategory($scope.categoryId)
                                .success(function (data) {
                                    // kendo grid callback
                                    e.success(data);
                                });
                        }
                    }
                },
                pageable: {
                    numeric: false,
                    previousNext: false,
                    messages: {
                        display: "产品总计: {2}"
                    }
                },
                sortable: true,
                height: 620,
                filterable: false,
                columns: [
                    //{
                    //    field: "primaryName", title: "名称1",
                    //    template: '<a class="c-black" ng-href="\\#/products/edit/#=productId#">#= primaryName #</a>'
                    //},
                    // { field: "secondaryName", title: "名称2" },
                    {
                        field: "primaryName", title: "名称",
                        template: "<div class='product-grid-img'" +
                                        "style='background-image: url(#:gbmono.img_root_path + '/' + data.imgUrl#);'></div>" +
                                    "<div class='product-grid-name'><a ng-href='\\#/products/edit/#=productId#'>#: primaryName #</a></div>"
                    },
                    { field: "brandName", title: "品牌 (制造商)" },
                    { field: "productCode", title: "产品代码", width: 80 },
                    { field: "barCode", title: "条形码", width: 150 },
                    {
                        field: "price", title: "价格", width: 100,
                        template: "#= '￥' + kendo.toString(kendo.toString(price), 'n0') #"
                    },
                    {
                        field: "activationDate", title: "上架日期", width: 100,
                        template: "#= kendo.toString(kendo.parseDate(activationDate), 'yyyy-MM-dd') #"
                    },
                    {
                        field: "expiryDate", title: "结束日期", width: 100,
                        template: "#= expiryDate == null ? '' : kendo.toString(kendo.parseDate(expiryDate), 'yyyy-MM-dd') #"
                    },
                    {
                        template: '<a class="btn btn-xs btn-info" ng-href="\\#/products/edit/#=productId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 100
                    }
                ]
            };
        }

        function getCategory(id) {
            categoryDataFactory.getById(id)
                .success(function (data) {
                    $scope.category = data; 
                });
        }
    }
})(angular.module('gbmono'));

/*
    product search page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'productDataFactory'];

    // create controller
    module.controller('productSearchController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, productDataFactory) {        
        $scope.search = {};
        // default search by barcode
        $scope.search.type = "1";

        // post search model
        $scope.searchModel = {};
        // page init
        init();

        // reload data
        $scope.reload = function () {
            $scope.searchModel = {
                barCode: $scope.search.type == 1 ? $scope.search.value : "",
                fullProductCode: $scope.search.type == 2 ? $scope.search.value : ""
            };

            
            // parent grid
            $scope.grid.dataSource.read();
        }

        function init() {
            // data return is null as the init search model is empty
            // data will be retreived based on the new search criteria when search button is clicked
            bindProductGrid();
        }

        // retreive product data and binding it into kendo grid
        function bindProductGrid() {
            // init kendo ui grid with product data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            productDataFactory.search($scope.searchModel)
                                .success(function (data) {
                                    // kendo grid callback
                                    e.success(data);
                                });
                        }
                    }
                },
                //pageable: {
                //    numeric: false,
                //    previousNext: false,
                //    messages: {
                //        display: "产品总计: {2}"
                //    }
                //},
                sortable: true,
                height: 580,
                filterable: false,
                columns: [
                    {
                        field: "primaryName", title: "名称",
                        template: "<div class='product-grid-img'" +
                                        "style='background-image: url(#:gbmono.img_root_path + '/' + data.imgUrl#);'></div>" +
                                    "<div class='product-grid-name'><a ng-href='\\#/products/edit/#=productId#'>#: primaryName #</a></div>"
                    },
                    { field: "brandName", title: "品牌 (制造商)" },
                    { field: "productCode", title: "产品代码", width: 80 },
                    { field: "barCode", title: "条形码", width: 150 },
                    {
                        field: "price", title: "价格", width: 100,
                        template: "#= '￥' + kendo.toString(kendo.toString(price), 'n0') #"
                    },
                    {
                        field: "createdDate", title: "创建日期", width: 100,
                        template: "#= kendo.toString(kendo.parseDate(createdDateTime), 'yyyy-MM-dd') #"
                    },
                    {
                        field: "activationDate", title: "上架日期", width: 100,
                        template: "#= kendo.toString(kendo.parseDate(activationDate), 'yyyy-MM-dd') #"
                    },
                    //{
                    //    field: "expiryDate", title: "结束日期", width: 100,
                    //    template: "#= expiryDate == null ? '' : kendo.toString(kendo.parseDate(expiryDate), 'yyyy-MM-dd') #"
                    //},
                    {
                        template: '<a class="btn btn-xs btn-info" ng-href="\\#/products/edit/#=productId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 100
                    }
                ]
            };
        }
    }
})(angular.module('gbmono'));
