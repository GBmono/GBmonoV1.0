/*
    ranking product controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService',
                    'utilService',
                    'productDataFactory',
                    'categoryDataFactory'];

    // create controller
    module.controller('productRankingController', ctrl);

    // controller body
    function ctrl(pluginService,
                  utilService,
                  productDataFactory,
                  categoryDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // products
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // categories
        vm.categories = [];
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;

        // page init
        init();

        function init() {
            // auto-move into the top of the screen to focus on the begininig of product list
            utilService.scrollToTop();

            // get ranking products
            getProducts();

            // get categories
            getCategories();
        }

        // get ranking products
        function getProducts() {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "60px;" });
            // get ranking product 
            productDataFactory.getByRanking()
                .success(function (data) {
                    vm.products = data;
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#products');
                });
        }

        // get categories
        function getCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    vm.categories = data;//console.log(data);
                });
        }
    }
})(angular.module('gbmono'));
