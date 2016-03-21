/*
    product list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$location',
                    'pluginService',
                    'utilService',
                    'productDataFactory',
                    'categoryDataFactory'];

    // create controller
    module.controller('productListController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $location,
                  pluginService,
                  utilService,
                  productDataFactory,
                  categoryDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // products
        vm.products = [];
        //  third cateogories (tags in filter)
        vm.thirdCategories = [];
        // category menu
        vm.menu = {};
        // optional subcategory
        vm.subCate = {};
        // second category id (optional)
        vm.secondCateId = 0;
        // third category id (optional)
        vm.thirdCateId = 0;
        // category brands
        vm.categoryBrands = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;
        // retreive top category id from route params
        var topCategoryId = $routeParams.id ? parseInt($routeParams.id) : 0;

        // view event handlers
        // load more products
        vm.loadProducts = function () {
            // get more products
            // if third category id exists
            if (vm.thirdCateId != 0 && !isNaN(vm.thirdCateId)) {
                getProducts(vm.thirdCateId,
                            vm.paging.pageIndex, // first page
                            vm.paging.pageSize); // page size
            }
            else {
                // load products by top category or sub category
                getProducts(vm.secondCateId == 0 ? topCategoryId : vm.secondCateId,
                            vm.paging.pageIndex, // first page
                            vm.paging.pageSize); // page size

            }

        };

        // attach third category id into the filters
        vm.filter = function (secondCateId, thirdCateId) {
            var params = secondCateId + ',' + thirdCateId;
            var filterUrl = '#/categories/' + topCategoryId + '/products/?filters=' + params;
            // redirect into current page with new filter params
            window.location = filterUrl;
        };

        // page init
        init();

        function init() {
            // auto-move into the top of the screen to focus on the begininig of product list
            utilService.scrollToTop();

            if (topCategoryId != 0 && !isNaN(topCategoryId)) {
                // extract sub cateid and third cate id from url filters if exists
                retreiveUrlFilter();

                // if third category id exists
                if (vm.thirdCateId != 0 && !isNaN(vm.thirdCateId)) {
                    getProducts(vm.thirdCateId,
                                vm.paging.pageIndex, // first page
                                vm.paging.pageSize); // page size
                }
                else {
                    // load products by top category or sub category
                    getProducts(vm.secondCateId == 0 ? topCategoryId : vm.secondCateId,
                                vm.paging.pageIndex, // first page
                                vm.paging.pageSize); // page size

                }

                // category menu list with expanded top category
                getCategoryMenu(topCategoryId);

                // load third categories (filter tags) by top category or second category 
                getThirdCategories(vm.secondCateId == 0 ? topCategoryId : vm.secondCateId);

                // get category brands
                getCategoryBrands(topCategoryId);
            }
        }

        // get products by category
        function getProducts(categoryId, pageIndex, pageSize) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "60px;" });
            // get product data
            productDataFactory.getByCategory(categoryId, pageIndex, pageSize)
                .success(function (data) {
                    // add products into collection
                    vm.products = vm.products.concat(data); 
                    // add page index
                    vm.paging.pageIndex++;
                    // if no more products 
                    if (data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                    }
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#products');
                });
        }

        function getThirdCategories(categoryId) {
            categoryDataFactory.getThirdCates(categoryId)
                .success(function (data) {
                    vm.thirdCategories = data;
                });
        }

        function getCategoryBrands(categoryId) {
            categoryDataFactory.getBrands(categoryId)
                .success(function (data) {
                    vm.categoryBrands = data;
                });
        }

        // get caetgory menu
        // expanded current top category with subitems, and unselected collapsed menuitems
        function getCategoryMenu(topCateId) {
            categoryDataFactory.getMenu(topCateId)
                .success(function (data) {
                    vm.menu = data;
                    // if subcategory is not null
                    // get the subcateory
                    if (vm.secondCateId !== 0) {
                        for (var i = 0; i < vm.menu.expandedItem.subItems.length; i++) {
                            if (vm.menu.expandedItem.subItems[i].categoryId === vm.secondCateId) {
                                vm.subCate = vm.menu.expandedItem.subItems[i];
                            }
                        }
                    }
                });
        }

        // get filter ids from route 
        function retreiveUrlFilter() {
            var filters = $location.search().filters;
            if (filters && filters != '') {
                // split by comma
                var arrParams = filters.split(',');
                // only second cate id
                if (arrParams.length > 0) {
                    vm.secondCateId = parseInt(arrParams[0]);
                }
                // third cate
                if (arrParams.length == 2) {
                    vm.thirdCateId = parseInt(arrParams[1]);
                }
            }
        }
    }
})(angular.module('gbmono'));
