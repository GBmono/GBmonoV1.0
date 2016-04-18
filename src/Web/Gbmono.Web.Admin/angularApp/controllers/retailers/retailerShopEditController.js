/*
    retailer shop edit controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', '$routeParams', 'pluginService', 'retailerShopsDataFactory'];

    // create controller
    module.controller('retailerShopEditController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, retailerShopsDataFactory) {
        var retailerShopId = $routeParams.retailerShopId ? parseInt($routeParams.retailerShopId) : 0;


        // page init
        init();



        function init() {
            getRetailerShop(retailerShopId);
        }

        function getRetailerShop(id) {
            retailerShopsDataFactory.getById(id)
                .success(function (data) {
                    $scope.editRetailerShop = data;
                });
        }
      
    }
})(angular.module('gbmono'));