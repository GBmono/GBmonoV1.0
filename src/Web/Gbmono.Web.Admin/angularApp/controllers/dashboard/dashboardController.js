/*
    dashboard page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'pluginService',
                    'statsDataFactory',
                    'productDataFactory'];

    // create controller
    module.controller('dashboardController', ctrl);

    // controller body
    function ctrl($scope,
                  pluginService,
                  statsDataFactory,
                  productDataFactory) {
        // site stats
        $scope.siteStatsItems = [];
        // product count chart series
        $scope.productCountItems = [];
        
        // page init
        init();

        function init() {
            // site stats
            getSiteStats();

            // product count chart
            getProductCount();
        }

        // site stats
        function getSiteStats() {
            // show data loading
            pluginService.showDataLoading('#widget_site');
            // call service
            statsDataFactory.getSiteStats()
                .success(function (data) {
                    $scope.siteStatsItems = data;
                    // close data loading
                    pluginService.closeDataLoading('#widget_site');
                });
        }

        // product count chart by category
        function getProductCount() {
            // show data loading
            pluginService.showDataLoading('#widget_product_count');
            // call service
            productDataFactory.getCountByCategory()
                .success(function (data) {
                    $scope.productCountItems = data;
                    // close data loading
                    pluginService.closeDataLoading('#widget_product_count');
                });
        }
    }
})(angular.module('gbmono'));