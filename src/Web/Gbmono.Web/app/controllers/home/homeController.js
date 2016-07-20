/*
   home page controller
*/
(function (module) {
    // inject the controller params
    // todo: remove page data factory once article data is ready in db
    ctrl.$inject = ['$filter',
                    'productDataFactory',
                    'articleDataFactory',
                    'pageDataFactory',
                    'pluginService'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl($filter,
                  productDataFactory,
                  articleDataFactory,
                  pageDataFactory,
                  pluginService) {
        var vm = this;
        // new products
        vm.products = [];
        // articles // todo: to be deprecated
        vm.articles = [];
        // articles 2
        vm.articles2 = [];
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

            // date range, last 14 days
            var to = $filter('date')(new Date(), 'yyyy-MM-dd')
            var from = $filter('date')(new Date().setDate(new Date().getDate() - 14), 'yyyy-MM-dd');

            // get new products
            getNewProducts();

            // get latest articles
            getArticles();
            getArticlesByDate(from, to);
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

        // get new articles
        function getArticles() {
            pageDataFactory.getArticles()
                .success(function (data) {
                    vm.articles = data;
                });
        }

        // get articles by date
        function getArticlesByDate(from, to) {
            articleDataFactory.getByDate(from, to)
                .success(function (data) {
                    vm.articles2 = data; console.log(data);
                });
        }
    }

})(angular.module('gbmono'));
