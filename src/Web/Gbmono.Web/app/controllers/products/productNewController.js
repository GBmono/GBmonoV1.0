/*
    new products list controller
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
    module.controller('productNewController', ctrl);

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
        // categories
        vm.categories = [];

        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if loading starts
        vm.isLoadingStarts = false;
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;

        // page init
        init();

        function init() {
            // auto-move into the top of the screen to focus on the begininig of product list
            utilService.scrollToTop();

            // load all categories
            getCategories();

            // load products by category
            getNewProducts(vm.paging.pageIndex, // first page
                           vm.paging.pageSize); // page size

            // attach scroll event handler
            angular.element(window).unbind('scroll').scroll(function () {
                var top = angular.element(window).scrollTop();
                var bottom = angular.element(document).height() - angular.element(window).height() - 50;
                // when it still has more data to be loaded
                if (top > bottom && !vm.isAllDataLoaded) {
                    // avoild sending multiple requests
                    if (!vm.isLoadingStarts) {
                        // load more data with page index and page size
                        getNewProducts(vm.paging.pageIndex, // first page
                                       vm.paging.pageSize); // page size
                    }                                        
                }
            });
        }

        function getCategories() {
            categoryDataFactory.getAll()
                .success(function (data) {
                    vm.categories = data;
                });
        }

        // get products by category
        function getNewProducts(pageIndex, pageSize) {
            // loading starts
            vm.isLoadingStarts = true;

            // data loading indicator
            pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "60px;" });

            // get product data
            productDataFactory.getNewProducts(pageIndex, pageSize)
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

                    // loading finishes
                    vm.isLoadingStarts = false;

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#products');
                });
        }

        // get detailed category info
        function getCurrentCategory(categoryId) {
            categoryDataFactory.getById(categoryId)
                .success(function (data) {
                    vm.selectedCategory = data;
                });
        }

        // get the brands of the current selected category
        function getCategoryBrands(categoryId, levelId) {
            categoryDataFactory.getBrands(categoryId, levelId)
                .success(function (data) {
                    vm.categoryBrands = data;
                });
        }

    }
})(angular.module('gbmono'));
