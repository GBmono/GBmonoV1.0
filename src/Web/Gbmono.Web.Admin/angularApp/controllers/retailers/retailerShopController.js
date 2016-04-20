/*
    retailer shop list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'pluginService',
                    'retailerDataFactory',
                    'retailerShopDataFactory',
                    'locationDataFactory'];

    // create controller
    module.controller('retailerShopListController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, retailerDataFactory, retailerShopDataFactory, locationDataFactory) {
        // page init
        init();

        // retailers
        $scope.retailers = [];
        // states
        $scope.states = [];
        // cities
        $scope.cities = [];
        // retail shops
        $scope.shops = [];
        // search model
        $scope.searchModel = {
            retailerId: -1,
            cityId: -1
        };

        // update event handelr

        function init() {
            // get retailers
            getRetailers();

            // get states, for stage 1, jp only
            getStates(1);

            bindShopsGrid();
        }

        // load cities by state
        $scope.stateChanged = function () {
            if ($scope.searchModel.stateId && $scope.searchModel.stateId != '') {
                getCities($scope.searchModel.stateId);
            }
        };

        // get shops
        $scope.search = function () {
            $scope.grid.dataSource.read();
        };


        // load retailers
        function getRetailers() {
            retailerDataFactory.getAll()
                .success(function (data) {
                    $scope.retailers = data;
                });
        }

        // load states
        function getStates(countryId) {
            locationDataFactory.getStates(countryId)
                .success(function (data) {
                    $scope.states = data;
                });
        }

        // load cities
        function getCities(stateId) {
            locationDataFactory.getCities(stateId)
                .success(function (data) {
                    $scope.cities = data;
                    // load finishes
                });
        }

        function bindShopsGrid() {
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            retailerShopDataFactory.getbyRetailerCity($scope.searchModel.retailerId, $scope.searchModel.cityId)
                                .success(function (data) {
                                    e.success(data);
                                });
                        }
                    }
                },
                pageable: {
                    numeric: false,
                    previousNext: false,
                    messages: {
                        display: "总计: {2}"
                    }
                },
                sortable: true,
                height: 650,
                filterable: false,
                columns: [
                    {
                        field: "name", title: "名称", width: 250,
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/retailershops/edit/#=retailShopId#">#= name #</a>',
                    },
                    {
                        field: "displayName", title: "显示名", width: 250,
                    },
                    {
                        field: "address", title: "地址", width: 250
                    },
                    {
                        field: "phone", title: "电话", width: 150
                    },
                    {
                        field: "taxFree", title: "免税", width: 80,
                        template: "<input type='checkbox' disabled='true' ng-model='dataItem.taxFree' />",
                    },
                    {
                        field: "unionpay", title: "银联", width: 80,
                        template: "<input type='checkbox' disabled='true' ng-model='dataItem.unionpay'/>",
                    },
                    {
                        field: "enabled", title: "激活", width: 80,
                        template: "<input type='checkbox' disabled='true' ng-model='dataItem.enabled' />",
                    }

                ]
            };
        }

    }
})(angular.module('gbmono'));