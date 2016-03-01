/*
    product detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams', 'pluginService', 'productDataFactory', 'categoryDataFactory'];

    // create controller
    module.controller('productDetailController', ctrl);

    // controller body
    function ctrl($routeParams, pluginService, productDataFactory, categoryDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // current product
        vm.product = {};
        // category menu
        vm.menu = {};

        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // retreive category id from route params
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;

        init();

        function init() {
            if (productId !== 0) {
                // get product by id
                getProduct(productId);
            }
        }

        function getProduct(productId) {
            productDataFactory.getById(productId)
                .success(function (data) {
                    // get current product
                    vm.product = data;
                    // init img thumb gallery
                    pluginService.productDetailGallery(4000);
                    // get category menu by top category id
                    getCategoryMenu(vm.product.category.parentCategory.parentId);
                });
        }

        // get caetgory menu
        // expanded current top category with subitems, and unselected collapsed menuitems
        function getCategoryMenu(topCateId) {
            categoryDataFactory.getMenu(topCateId)
                .success(function (data) {
                    vm.menu = data;
                });
        }
    }
})(angular.module('gbmono'));