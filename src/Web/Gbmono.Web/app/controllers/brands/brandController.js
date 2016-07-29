/*
    brand list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'brandDataFactory'];

    // create controller
    module.controller('brandListController', ctrl);

    // controller body
    function ctrl(pluginService, brandDataFactory) {
        var vm = this;
        // grouped brands
        vm.groups = [];


        init(); // page init 

        function init() {
            // load brands
            getBrands();
        }

        function getBrands() {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#brandView', { left: "50%", top: "180px;" });
            // call service
            brandDataFactory.getAll()
                .success(function (data) {
                    vm.groups = data;
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#brandView');
                });
        }
    }
})(angular.module('gbmono'));

/*
    brand detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    'pluginService',
                    'brandDataFactory',
                    'productDataFactory'];

    // create controller
    module.controller('brandDetailController', ctrl);

    // controller body
    function ctrl($routeParams,
                  pluginService,
                  brandDataFactory,
                  productDataFactory) {
        var vm = this;

        // brand
        vm.brand = {};
        // brand image root
        vm.brandImgRoot = gbmono.img_brand_root_path;

        // brand products
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };

        // brand id from url route 
        var brandId = $routeParams.brandId ? parseInt($routeParams.brandId) : 0;

        init(); // page init 

        // event handlers
        vm.load = function () {
            getBrandProducts(brandId,
                             vm.paging.pageIndex, // page index
                             vm.paging.pageSize); // page size
        };

        function init() {
            // get brand by id
            getBrand(brandId);

            // get products
            getBrandProducts(brandId, 
                             vm.paging.pageIndex, // first page
                             vm.paging.pageSize); // page size

            // init bootstrap tab
            pluginService.tab();
        }

        function getBrand(brandId) {
            brandDataFactory.getById(brandId)
                .success(function (data) {
                    vm.brand = data;
                });
        }

        function getBrandProducts(brandId, pageIndex, pageSize) {
            // data loading indicator
            pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "60px;" });

            // call service
            productDataFactory.getByBrand(brandId, pageIndex, pageSize)
                .success(function (data) {
                    // add products into collection
                    vm.products = vm.products.concat(data);

                    // add page index
                    vm.paging.pageIndex++;

                    // if no more products 
                    if (data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                    }

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#products');

                    // vm.products = data;
                });
        }
    }
})(angular.module('gbmono'));