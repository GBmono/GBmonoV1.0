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
        // sub category id (optional)
        var subCateId = $location.search().subcateid ? parseInt($location.search().subcateid) : 0;

        // view event handlers
        vm.loadProducts = function () {
            // get products
            getProducts(subCateId == 0 ? topCategoryId : subCateId,
                            vm.paging.pageIndex, // page index
                            vm.paging.pageSize); // page size
        };

        // page init
        init();

        function init() {
            // auto-move into the top of the screen to focus on the begininig of product list
            utilService.scrollToTop();

            if (topCategoryId != 0) {
                // load products by top category or sub category
                getProducts(subCateId == 0 ? topCategoryId : subCateId,
                            vm.paging.pageIndex, // first page
                            vm.paging.pageSize); // page size

                // category menu list with expanded top category
                getCategoryMenu(topCategoryId);

                // third categories in filter tag
                getThirdCategories(subCateId == 0 ? topCategoryId : subCateId);

                // get category brands
                getCategoryBrands(topCategoryId);
            }
        }

        // get products by category
        function getProducts(categoryId, pageIndex, pageSize) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#products');
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
                    if (subCateId !== 0) {
                        for (var i = 0; i < vm.menu.expandedItem.subItems.length; i++) {
                            if (vm.menu.expandedItem.subItems[i].categoryId === subCateId) {
                                vm.subCate = vm.menu.expandedItem.subItems[i];
                            }
                        }
                    }
                });
        }
    }
})(angular.module('gbmono'));
