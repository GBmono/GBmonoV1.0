/*
    retailer shop edit controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [
        '$scope', '$routeParams', 'pluginService', 'retailerShopDataFactory', 'locationDataFactory'];

    // create controller
    module.controller('retailerShopEditController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, retailerShopDataFactory, locationDataFactory) {
        var retailerShopId = $routeParams.retailerShopId ? parseInt($routeParams.retailerShopId) : 0;
        $scope.editShopStateId = null;
        // states
        $scope.states = [];
        // cities
        $scope.cities = [];

        // page init
        init();

        function init() {
            getStates(1);

            getRetailerShop(retailerShopId);
        }

        // load cities by state for edit
        $scope.stateChangedForEdit = function () {
            getCitiesForEdit($scope.editShopStateId);
        };

        // load states
        function getStates(countryId) {
            locationDataFactory.getStates(countryId)
                .success(function (data) {
                    $scope.states = data;
                });
        }

        // load cities for edit
        function getCitiesForEdit(stateId) {
            if (stateId) {
                locationDataFactory.getCities(stateId)
                    .success(function (data) {
                        $scope.citiesForEdit = data;
                    });
            } else {
                $scope.citiesForEdit = [];
            }
        }

      


        function getRetailerShop(id) {
            retailerShopDataFactory.getById(id)
                .success(function (data) {
                    $scope.editRetailerShop = data;
                    $scope.editShopStateId = data.city.state.stateId;

                    getCitiesForEdit(data.city.state.stateId);
                });
        }

        $scope.update = function () {
            update($scope.editRetailerShop);
        };

        function update(retailerShop) {
            retailerShopDataFactory.update(retailerShop)
                .success(function () {
                    pluginService.notify('店铺更新成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
      
    }
})(angular.module('gbmono'));