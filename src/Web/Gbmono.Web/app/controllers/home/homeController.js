/*
   home page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['productDataFactory',
                    'pageDataFactory',
                    'pluginService'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl(productDataFactory,
                  pageDataFactory,
                  pluginService) {
        var vm = this;
        // new products
        vm.products = [];
        // articles
        vm.articles = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        // call page init function
        init();

        function init() {
            // enable jquery slider
            // call jquery initialization
            pluginService.slider();

            // get new products
            getNewProducts();

            // get latest articles
            getArticles();
        }

        // get new products
        function getNewProducts() {
            // get new arrived products
            // load first 12 new products 
            productDataFactory.getNewProducts(1, 6)
                .success(function (data) {
                    // retreive data
                    vm.products = data;
                });
        }

        // get new articles
        function getArticles() {
            pageDataFactory.getArticles()
                .success(function (data) {
                    vm.articles = data;
                });
        }
    }

})(angular.module('gbmono'));
