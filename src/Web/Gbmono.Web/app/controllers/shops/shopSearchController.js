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
    module.controller('shopSearchController', ctrl);

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
        //vm.showMap = function (addr) {
        //    // google url
        //    // var googleUrl = 'https://www.google.com/maps?q=';
        //    var googleUrl = 'http://ditu.google.cn/maps?q=';
        //    vm.mapUrl = $sce.trustAsResourceUrl(googleUrl + addr + "&output=embed");

        //    // show modal
        //    pluginService.modal('#map');
        //};

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
            pluginService.showDataLoadingIndicator('#shopSearchView', { left: "50%", top: "180px;" });

            // vm.pagedRequest.data = model;
            // google url
            // var googleUrl = 'https://www.google.com/maps?q=';
            var googleUrl = 'http://ditu.google.cn/maps?q=';

            // call data facotry
            retailerShopDataFactory.search(model)
                .success(function (data) {
                    // vm.shops = data;                    
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