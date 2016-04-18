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

        $scope.update = function () {
            update($scope.editRetailerShop);
        };

        function update(retailerShop) {
            retailerShopsDataFactory.update(retailerShop)
                .success(function () {
                    pluginService.notify('店铺更新成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
      
    }
})(angular.module('gbmono'));