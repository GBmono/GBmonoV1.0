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
        // categories
        vm.categories = [];
        // category brands
        vm.categoryBrands = [];
        // selected category
        vm.selectedCategory = {};

        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if data requesting starts
        vm.isLoadingStarts = false;
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;

        // retreive category id from route params
        var categoryId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // retreive sub id (category level) from route prams
        var levelId = $routeParams.subId ? parseInt($routeParams.subId) : 1;

        // view event handlers
        // load more products
        vm.loadProducts = function () {
            //// get more products
            //// if third category id exists
            //if (vm.thirdCateId != 0 && !isNaN(vm.thirdCateId)) {
            //    getProducts(vm.thirdCateId,
            //                vm.paging.pageIndex, // first page
            //                vm.paging.pageSize); // page size
            //}
            //else {
            //    // load products by top category or sub category
            //    getProducts(vm.secondCateId == 0 ? topCategoryId : vm.secondCateId,
            //                vm.paging.pageIndex, // first page
            //                vm.paging.pageSize); // page size

            //}

        };

        // page init
        init();

        function init() {
            // auto-move into the top of the screen to focus on the begininig of product list
            utilService.scrollToTop();

            // load all categories
            getCategories();

            // load products by category
            getProducts(categoryId,
                        vm.paging.pageIndex, // first page
                        vm.paging.pageSize); // page size

            // get the current category model
            getCurrentCategory(categoryId);

            // load brands
            getCategoryBrands(categoryId, levelId);

            // attach scroll event handler
            angular.element(window).unbind('scroll').scroll(function () {
                var top = angular.element(window).scrollTop();
                var bottom = angular.element(document).height() - angular.element(window).height() - 50;
                // when it still has more data to be loaded
                if (top > bottom && !vm.isAllDataLoaded) {
                    // avoild sending multiple requests
                    if (!vm.isLoadingStarts) {
                        // load more products when scrolling at the bottom
                        getProducts(categoryId,
                                    vm.paging.pageIndex, // first page
                                    vm.paging.pageSize); // page size
                    }
                }
            });
        }
        
        // load all categories
        function getCategories(){
            categoryDataFactory.getAll()
                .success(function(data){
                    vm.categories = data;
                });
        }

        // get products by category
        function getProducts(categoryId, pageIndex, pageSize) {
            // data requesting starts
            vm.isLoadingStarts = true;
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

                    // data requesting finishes
                    vm.isLoadingStarts = false;
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
