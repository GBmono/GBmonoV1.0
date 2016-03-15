/*
    dashboard page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'categoryDataFactory',
                    'productDataFactory'];

    // create controller
    module.controller('dashboardController', ctrl);

    // controller body
    function ctrl($scope, categoryDataFactory, productDataFactory) {
        $scope.productCountItems = [];
        
        // page init
        init();

        function init() {
            getProductCount();
        }

        // product count by category
        function getProductCount() {
            productDataFactory.getCountByCategory()
                .success(function (data) {
                    $scope.productCountItems = data;
                });
        }
    }
})(angular.module('gbmono'));