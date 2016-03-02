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
        // product images
        vm.productImages = [];
        // instruction images
        vm.instrutionImages = [];
        // extra info images
        vm.extraImages = [];
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

        // get product details by id
        function getProduct(productId) {
            productDataFactory.getById(productId)
                .success(function (data) {
                    // get current product
                    vm.product = data;
                    // filter image by type id
                    filterImages(data.images);
                    // get category menu by top category id
                    getCategoryMenu(vm.product.category.parentCategory.parentId);
                    // init img thumb gallery
                    pluginService.productDetailGallery(4000);
                    // init bootstrap tab
                    pluginService.tab();
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

        // filter images by image type
        function filterImages(images) {
            for (var i = 0; i < images.length; i++) {
                // type id
                var imgTypeId = images[i].productImageTypeId
                // product img
                if (imgTypeId == gbmono.imgType.product) {
                    vm.productImages.push(images[i]);
                }
                else if (imgTypeId == gbmono.imgType.descritpion) {
                    // todo
                }
                else if (imgTypeId == gbmono.imgType.instruction) {
                    vm.instrutionImages.push(images[i]);
                }
                else if (imgTypeId == gbmono.imgType.extra) {
                    vm.extraImages.push(images[i]);
                }
            }
        }
    }
})(angular.module('gbmono'));