/*
    home controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'pageDataFactory', 'productDataFactory'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl(pluginService, pageDataFactory, productDataFactory) {
        var vm = this;
        // sliders
        vm.sliders = [];
        // products
        vm.products = [];
        // articles 
        vm.articles = [];
        // news
        vm.news = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        // page init
        init();

        function init() {
            // load sliders
            getSilders();

            // load new products
            getNewProducts();

            // load articles
            getArticles();

            // load news
            getNews();
        }

        function getSilders() {
            // load slider data
            pageDataFactory.getSilders()
                .success(function (data) {
                    // once data is ready
                    vm.sliders = data;
                    // call jquery initialization
                    pluginService.slider();
                });
        }

        function getNewProducts() {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#productsContainer', {left: "42%", top: "90px;"});
            // get new arrived products
            // load first 12 new products 
            productDataFactory.getNewProducts(1, 8)
                .success(function (data) {
                    // retreive data
                    vm.products = data;
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#productsContainer');
                });
        }

        function getArticles() {
            pageDataFactory.getArticles()
                .success(function (data) {
                    vm.articles = data;
                });
        }

        function getNews() {
            pageDataFactory.getNews()
                .success(function (data) {
                    vm.news = data;
                });
        }
    }
})(angular.module('gbmono'));
