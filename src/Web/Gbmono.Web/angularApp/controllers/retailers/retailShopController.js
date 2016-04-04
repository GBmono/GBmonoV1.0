/*
    shop list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$sce',
                    'pluginService',
                    'retailerDataFactory',
                    'retailerShopDataFactory'];

    // create controller
    module.controller('retailShopController', ctrl);

    // controller body
    function ctrl($routeParams, $sce, pluginService, retailerDataFactory, retailerShopDataFactory) {
        var vm = this;
        // retailers
        vm.retailers = [];
        // retail shops
        vm.shops = [];
        // search model
        vm.searchModel = {};
        // map url
        vm.mapUrl = '';

        // if any retailer id passed by url
        var retailerId = $routeParams.retailerId ? parseInt($routeParams.retailerId) : 0;

        // page init 
        init(); 

        function init() {
            // load retailers
            getRetailers();
        }

        /* event handlers*/
        // search shops
        vm.search = function () {
            searchShops(vm.searchModel);
        };

        // show map
        vm.showMap = function (addr) {
            vm.mapUrl = $sce.trustAsResourceUrl('https://www.google.com/maps?q=' + addr + "&output=embed");
            // console.log(vm.mapUrl);
            // show modal
            pluginService.modal('#map');
        };

        function getRetailers() {
            retailerDataFactory.getAll()
                .success(function (data) {
                    vm.retailers = data;

                    // if retailer id is passed
                    if (retailerId != 0) {
                        vm.searchModel.retailerId = retailerId;
                    }
                    else {
                        // default retailer
                        vm.searchModel.retailerId = data[0].retailerId;
                    }
                });
        }

        function searchShops(model) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#shops', { left: "50%", top: "180px;" });

            // call data facotry
            retailerShopDataFactory.search(model)
                .success(function (data) {
                    vm.shops = data;

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#shops');
                });
        }
    }
})(angular.module('gbmono'));