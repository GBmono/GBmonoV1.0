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

        // attach third category id into the filters
        vm.filter = function (secondCateId, thirdCateId) {
            $location.search('filters', secondCateId + ',' + thirdCateId);
            //var params = secondCateId + ',' + thirdCateId;
            //var filterUrl = '#/categories/' + topCategoryId + '/products/?filters=' + params;
            //// redirect into current page with new filter params
            //window.location = filterUrl;
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
            console.log(categoryId, levelId);
            // load brands
            getCategoryBrands(categoryId, levelId);            
        }
        
        function getCategories(){
            categoryDataFactory.getAll()
                .success(function(data){
                    vm.categories = data;
                });
        }

        // get products by category
        function getProducts(categoryId, pageIndex, pageSize) {
            // data loading indicator
            // pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "60px;" });
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
                    // pluginService.closeDataLoadingIndicator('#products');
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
