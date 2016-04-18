/*
    retailer shop list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$routeParams',
                    'pluginService',
                    'retailerShopsDataFactory'];

    // create controller
    module.controller('retailerShopListController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, retailerShopsDataFactory) {

        //retailerId
        var retailerId = $routeParams.retailerId ? parseInt($routeParams.retailerId) : 0;

        // page init
        init();


        // update event handelr

        function init() {
            bindShops(retailerId);
        }

        // retreive brand data and binding it into kendo grid
        function bindShops() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            retailerShopsDataFactory.getByShopsByRetailerId(retailerId)
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
                        template: "<input type='checkbox' disabled='true'  data-bind='checked: taxFree' #= taxFree ? checked='checked' : '' #/>",
                    },
                    {
                        field: "unionpay", title: "银联", width: 80,
                        template: "<input type='checkbox' disabled='true' data-bind='checked: unionpay' #= unionpay ? checked='checked' : '' #/>",
                    },
                    {
                        field: "enabled", title: "激活", width: 80,
                        template: "<input type='checkbox' disabled='true' data-bind='checked: enabled' #= enabled ? checked='checked' : '' #/>",
                    }
                    
                ]
            };
        }


    }
})(angular.module('gbmono'));