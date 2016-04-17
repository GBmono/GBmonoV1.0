/*
    product pin controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'pageDataFactory', 'productDataFactory'];

    // create controller
    module.controller('productPinController', ctrl);

    // controller body
    function ctrl(pluginService, pageDataFactory, productDataFactory) {
        var vm = this;
        // products
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        // page init
        init();

        function init() {
            // load new products
            getNewProducts();
        }

        function getNewProducts() {
            // show 'data loading' indicator
            pluginService.showDataLoadingIndicator('#columns', { left: "50%", top: "25px" });
            // get new arrived products
            // load first 12 new products 
            productDataFactory.getNewProducts(1, 72)
                .success(function (data) {
                    // retreive the data
                    vm.products = data;
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#columns');
                });
        }
    }
})(angular.module('gbmono'));
