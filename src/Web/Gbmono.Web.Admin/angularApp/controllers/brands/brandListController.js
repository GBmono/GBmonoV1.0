/*
    brand list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'brandDataFactory'];

    // create controller
    module.controller('brandListController', ctrl);

    // controller body
    function ctrl($scope, brandDataFactory) {

        // page init
        init();

        function init() {
            bindProductGrid();
        }

        // retreive brand data and binding it into kendo grid
        function bindProductGrid() {
            // init kendo ui grid with brand data
            $scope.mainGridOptions = {
                dataSource: {
                    transport: {
                        read: function (e) {
                            brandDataFactory.getAll()
                                .success(function (data) {
                                    // kendo grid callback
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
                    //{
                    //    field: "primaryName", title: "名称",
                    //    template: "<div class='product-grid-img'" +
                    //                    "style='background-image: url(#:gbmono.img_product_path + '/' + data.imgUrl#);'></div>" +
                    //                "<div class='product-grid-name'><a ng-href='\\#/products/edit/#=productId#'>#: primaryName #</a></div>"
                    //},
                    { field: "name", title: "名称" },
                    // { field: "description", title: "简介" },
                    {
                        template: '<a class="btn btn-xs btn-info" ng-href="\\#/brands/edit/#=brandId#"><i class="ace-icon fa fa-pencil bigger-120"></i></a>&nbsp;&nbsp;' +
                                  '<button class="btn btn-xs btn-danger" ng-click=""><i class="ace-icon fa fa-trash-o bigger-120"></i></button>', width: 100
                    }
                ]
            };
        }
    }
})(angular.module('gbmono'));