/*
    shop list browse controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$sce',
                    'pluginService',
                    'locationDataFactory',
                    'retailerDataFactory',
                    'retailerShopDataFactory'];

    // create controller
    module.controller('shopListController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $sce,
                  pluginService,
                  locationDataFactory,
                  retailerDataFactory,
                  retailerShopDataFactory) {
        var vm = this;
        // retailers
        vm.retailers = [];
        // states
        vm.states = [];
        // cities
        vm.cities = [];
        // retail shops
        vm.shops = [];
        // search model
        vm.searchModel = {};

        // map url
        // vm.mapUrl = '';

        // page init 
        init();

        function init() {
            // get retailers
            getRetailers();

            // get states, for stage 1, jp only
            getStates(1);
        }

        // load cities by state
        vm.stateChanged = function () {
            if (vm.searchModel.stateId && vm.searchModel.stateId != '') {
                getCities(vm.searchModel.stateId);
            }
        };

        // get shops
        vm.search = function () {
            getShops(vm.searchModel.retailerId, vm.searchModel.cityId);
        };

        // show map
        //vm.showMap = function (addr) {
        //    // google url
        //    // var googleUrl = 'https://www.google.com/maps?q=';
        //    var googleUrl = 'http://ditu.google.cn/maps?q=';
        //    vm.mapUrl = $sce.trustAsResourceUrl(googleUrl + addr + "&output=embed");

        //    // show modal
        //    pluginService.modal('#map');
        //};

        // load retailers
        function getRetailers() {
            retailerDataFactory.getAll()
                .success(function (data) {
                    vm.retailers = data;
                });
        }

        // load states
        function getStates(countryId) {
            locationDataFactory.getStates(countryId)
                .success(function (data) {
                    vm.states = data;
                });
        }

        // load cities
        function getCities(stateId) {
            locationDataFactory.getCities(stateId)
                .success(function (data) {
                    vm.cities = data;
                    // load finishes
                    // pluginService.closeDataLoadingIndicator('#shopSearchView');
                });
        }

        // get shops
        function getShops(retailerId, cityId) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#shopSearchView', { left: "50%", top: "180px;" });

            // google url
            // var googleUrl = 'https://www.google.com/maps?q=';
            var googleUrl = 'http://ditu.google.cn/maps?q=';

            // call data facotry
            retailerShopDataFactory.getByCity(retailerId, cityId)
                .success(function (data) {
                    for (var i = 0; i < data.length; i++) {
                        // build map url
                        data[i].mapUrl = $sce.trustAsResourceUrl(googleUrl + data[i].address + "&output=embed");
                    }

                    vm.shops = data;

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#shopSearchView');
                });
        }
    }
})(angular.module('gbmono'));