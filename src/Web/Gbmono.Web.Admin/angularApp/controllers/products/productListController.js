/*
    product browse by category page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'pluginService',
                    'categoryDataFactory',
                    'productDataFactory',
                    'tagDataFactory'];

    // create controller
    module.controller('productBrowseController', ctrl);

    // controller body
    function ctrl($scope,
                  $routeParams,
                  pluginService,
                  categoryDataFactory,
                  productDataFactory,
                  tagDataFactory) {
        // current category model
        $scope.category = {};

        // kendo ui grid binding options
        $scope.mainGridOptions = {};

        // kendo multiple selection options
        $scope.selectOptions = {};
        // selected product tag ids
        $scope.selectedTagIds = [];
        // selected product id
        $scope.selectedProductId = 0;
        // retreive category from route
        $scope.categoryId = $routeParams.id ? parseInt($routeParams.id) : 0;

        // page init
        init();

        // reload data
        $scope.reload = function () {
            // parent grid
            $scope.grid.dataSource.read();
        };

        // get tags
        $scope.getTags = function (productId) {           
            // set the product id
            $scope.selectedProductId = productId;

            // load product tags
            getProductTags(productId);

            // open tags window
            $scope.winTag.open();            
        };

        // save tags
        $scope.saveTags = function () {
            var model = {
                productId: $scope.selectedProductId,
                tagIds: $scope.selectedTagIds
            };

            // save tags
            productDataFactory.saveTags(model)
                .success(function () {
                    // close window
                    $scope.winTag.close();
                })
                .error(function (error) {       
                    pluginService.notify(error, 'error')
                });
            // console.log(model);
        };

        function init() {
            getCategory($scope.categoryId);

            // bind data
            bindProductGrid();

            // init multiple selection with empty data
            bindTagSelection();
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
                    {
                        field: "primaryName", title: "名称",
                        template: "<div class='product-grid-img'" +
                                        "style='background-image: url(#:gbmono.img_root_path + '/' + data.imgUrl#);'></div>" +
                                    "<div class='product-grid-name'><a ng-href='\\#/products/edit/#=productId#'>#: primaryName #</a></div>"
                    },
                    { field: "secondaryName", title: "名称2" },
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
                        template: '<a class="btn btn-xs btn-success" ng-click="getTags(dataItem.productId)"><i class="ace-icon fa fa-tags bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<a class="btn btn-xs btn-info" ng-href="\\#/products/edit/#=productId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
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
                            tagDataFactory.getAll()
                                .success(function (data) {
                                    // kendo selection callback
                                    e.success(data);
                                });
                        }
                    }
                }
            };
        }

        function getProductTags(productId) {
            // reset the selected  tag ids
            $scope.selectedTagIds = [];

            // load product tag 
            productDataFactory.getTags(productId)
                .success(function (data) {
                    // retreive the tag id
                    for (var i = 0; i < data.length; i++) {
                        $scope.selectedTagIds.push(data[i].tagId);
                    }
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
                    'pluginService',
                    'productDataFactory',
                    'tagDataFactory'];

    // create controller
    module.controller('productSearchController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, productDataFactory, tagDataFactory) {
        $scope.search = {};
        // default search by barcode
        $scope.search.type = "1";
        // post search model
        $scope.searchModel = {};

        // kendo multiple selection options
        $scope.selectOptions = {};
        // selected product tag ids
        $scope.selectedTagIds = [];
        // selected product id
        $scope.selectedProductId = 0;

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

        // get tags
        $scope.getTags = function (productId) {
            // set the product id
            $scope.selectedProductId = productId;

            // load product tags
            getProductTags(productId);

            // open tags window
            $scope.winTag.open();
        };

        // save tags
        $scope.saveTags = function () {
            var model = {
                productId: $scope.selectedProductId,
                tagIds: $scope.selectedTagIds
            };

            // save tags
            productDataFactory.saveTags(model)
                .success(function () {
                    // close window
                    $scope.winTag.close();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        };

        function init() {
            // data return is null as the init search model is empty
            // data will be retreived based on the new search criteria when search button is clicked
            bindProductGrid();

            // init multiple selection with empty data
            bindTagSelection();
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
                        template: '<a class="btn btn-xs btn-success" ng-click="getTags(dataItem.productId)"><i class="ace-icon fa fa-tags bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<a class="btn btn-xs btn-info" ng-href="\\#/products/edit/#=productId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                    }
                ]
            };
        }

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
                            tagDataFactory.getAll()
                                .success(function (data) {
                                    // kendo selection callback
                                    e.success(data);
                                });
                        }
                    }
                }
            };
        }

        function getProductTags(productId) {
            // reset the selected  tag ids
            $scope.selectedTagIds = [];

            // load product tag 
            productDataFactory.getTags(productId)
                .success(function (data) {
                    // retreive the tag id
                    for (var i = 0; i < data.length; i++) {
                        $scope.selectedTagIds.push(data[i].tagId);
                    }
                });
        }
    }
})(angular.module('gbmono'));
