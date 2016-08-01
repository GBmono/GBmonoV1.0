/*
   home page controller
*/
(function (module) {
    // inject the controller params
    // todo: remove page data factory once article data is ready in db
    ctrl.$inject = ['$filter',
                    'productDataFactory',
                    'articleDataFactory',
                    'pluginService'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl($filter,
                  productDataFactory,
                  articleDataFactory,
                  pluginService) {
        var vm = this;
        // new products
        vm.products = [];
        // recomend products
        vm.recommendProducts = [];
        // news
        vm.newsArticles = [];
        // shop articles
        vm.shopArticles = [];
        // product (brand) articles
        vm.productArticles = [];

        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;

        // call page init function
        init();

        function init() {
            // enable jquery slider
            // call jquery initialization
            pluginService.slider();

            // get new products
            getNewProducts();

            // get new articles
            getNewArticles();

            // get recommend products
            getRecommendProducts();
        }

        // get new articles
        function getNewArticles() {
            // load latest 6 items for each type of article
            var pageIndex = 1, pageSize = 6;

            // get marketing articles
            articleDataFactory.getByType(1, pageIndex, pageSize)
                .success(function (data) {
                    vm.newsArticles = data;
                });

            // get shop articles
            articleDataFactory.getByType(2, pageIndex, pageSize)
                .success(function (data) {
                    vm.shopArticles = data;
                });

            // get product (brand) articles
            articleDataFactory.getByType(3, pageIndex, pageSize)
                .success(function (data) {
                    vm.productArticles = data;
                });
        }

        // get new products
        function getNewProducts() {
            // get new arrived products
            // load first 12 new products 
            productDataFactory.getNewProducts(1, 8)
                .success(function (data) {
                    // retreive data
                    vm.products = data;
                });
        }

        // get recommend products
        function getRecommendProducts() {
            // todo: update the data method
            productDataFactory.getByCategory(38, 1, 8)
                .success(function (data) {
                    vm.recommendProducts = data;
                    // init the jquery slider
                    // after data is retreived
                    pluginService.snap();
                });
        }
    }

})(angular.module('gbmono'));
