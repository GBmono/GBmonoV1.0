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
        // edit shop
        $scope.editShop = {};
        // search model
        $scope.searchModel = {
            retailerId: -1,
            cityId: -1
        };
        $scope.editShopStateId = null;

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

        // load cities by state for edit
        $scope.stateChangedForEdit = function () {
            getCitiesForEdit($scope.editShopStateId);
        };

        // get shops
        $scope.search = function () {
            $scope.grid.dataSource.read();
        };

        // event hadlers
        $scope.showEdit = function (dataItem) {
            $scope.editShop = dataItem;

            $scope.editShopStateId = -1;
            // show window
            $scope.winEdit.open();
        };

        $scope.update = function (dataItem) {
            updateShop($scope.editShop);
            // show window
            $scope.winEdit.open();
        };

        // update shop
        function updateShop(shop) {
            retailerShopDataFactory.update(shop)
               .success(function (data) {
                   // reload data
                   $scope.grid.dataSource.read();
                   // close window
                   $scope.winEdit.close();
               })
               .error(function (error) {
                   pluginService.notify(error, 'error');
               });
        }

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
                });
        }

        // load cities for edit
        function getCitiesForEdit(stateId) {
            if (stateId) {
                locationDataFactory.getCities(stateId)
                    .success(function(data) {
                        $scope.citiesForEdit = data;
                    });
            } else {
                $scope.citiesForEdit = [];
            }
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
                        field: "name", title: "名称", width: 220,
                        template: '<i class="ace-icon fa fa-caret-right blue"></i> <a class="grey" ng-href="\\#/retailershops/edit/#=retailShopId#">#= name #</a>',
                    },
                    {
                        field: "displayName", title: "显示名", width: 220,
                    },
                    {
                        field: "address", title: "地址", width: 220
                    },
                    {
                        field: "phone", title: "电话", width: 120
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
                    },
                     {
                         template: '<button class="btn btn-xs btn-info" ng-click="showEdit(dataItem)"><i class="ace-icon fa fa-edit bigger-120"></i></button>&nbsp;&nbsp;' +
                                   '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 150
                     }


                ]
            };
        }

    }
})(angular.module('gbmono'));