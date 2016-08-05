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
    module.controller('retailShopBrowseController', ctrl);

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
        vm.searchModel = { };

        // map url
        vm.mapUrl = '';

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
        vm.showMap = function (addr) {
            // google url
            // var googleUrl = 'https://www.google.com/maps?q=';
            var googleUrl = 'http://ditu.google.cn/maps?q=';
            vm.mapUrl = $sce.trustAsResourceUrl(googleUrl + addr + "&output=embed");

            // show modal
            pluginService.modal('#map');
        };

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
                    pluginService.closeDataLoadingIndicator('#cities');
                });
        }

        // get shops
        function getShops(retailerId, cityId) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#shops', { left: "50%", top: "180px;" });

            // call data facotry
            retailerShopDataFactory.getByCity(retailerId, cityId)
                .success(function (data) {
                    vm.shops = data;
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#shops');
                });
        }
    }
})(angular.module('gbmono'));


/*
    shop list search controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$sce',
                    'pluginService',
                    'retailerDataFactory',
                    'retailerShopDataFactory'];

    // create controller
    module.controller('retailShopSearchController', ctrl);

    // controller body
    function ctrl($routeParams, $sce, pluginService, retailerDataFactory, retailerShopDataFactory) {
        var vm = this;        
        // retailers
        vm.retailers = [];
        // retail shops
        vm.shops = [];
        vm.pagedRequest = {
            //page number
            pageNumber: 1,
            //page size
            pageSize: 10
        };
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
            // google url
            // var googleUrl = 'https://www.google.com/maps?q=';
            var googleUrl = 'http://ditu.google.cn/maps?q=';
            vm.mapUrl = $sce.trustAsResourceUrl(googleUrl + addr + "&output=embed");

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

            vm.pagedRequest.data = model;
            // call data facotry
            retailerShopDataFactory.search(vm.pagedRequest)
                .success(function (data) {
                    vm.shops = data.data;

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#shops');
                });
        }
    }
})(angular.module('gbmono'));