/*
    home controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', 'pluginService', 'pageDataFactory', 'productDataFactory'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl($scope, pluginService, pageDataFactory, productDataFactory) {
        // sliders
        $scope.sliders = [];
        // products
        $scope.products = [];
        // news
        $scope.news = [];
        // product image root path
        $scope.imgRoot = gbmono.img_root_path;

        // page init
        init();

        function init() {
            // load sliders
            getSilders();

            // load new products
            getNewProducts();

            // load news
            getNews();
        }

        function getSilders() {
            // load slider data
            pageDataFactory.getSilders()
                .success(function (data) {
                    // once data is ready
                    $scope.sliders = data;
                    // call jquery initialization
                    pluginService.slider();
                });
        }

        function getNewProducts() {
            // get new arrived products
            // load first 12 new products 
            productDataFactory.getNewProducts(1, 12)
                .success(function (data) {
                    $scope.products = data;
                });
        }

        function getNews() {
            pageDataFactory.getNews()
                .success(function (data) {
                    $scope.news = data;
                });
        }
    }
})(angular.module('gbmono'));
