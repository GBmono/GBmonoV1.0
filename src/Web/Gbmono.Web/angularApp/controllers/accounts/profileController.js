/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location', 'utilService', 'userFavoriteDataFactory'];

    // create controller
    module.controller('profileController', ctrl);

    // controller body
    function ctrl($location, utilService, userFavoriteDataFactory) {
        var vm = this;
        // user favorite products
        vm.products = [];
        // user name
        vm.name = '';
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;
        
        // get token from local storage
        var token = utilService.getToken();
        
        init(); // page init 

        function init() {
            // if no token
            if (!token || token === '') {
                // redirect into login page
                $location.path('/login');
            }
            // get current user name
            vm.name = utilService.getUserName();
            // get user favorites
            getFavoriteProducts(token, vm.paging.pageIndex, vm.paging.pageSize);
        }

        function getFavoriteProducts(userToken, pageIndex, pageSize) {
            userFavoriteDataFactory.getFavoriteProducts(userToken, pageIndex, pageSize)
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
                });
        }

    }
})(angular.module('gbmono'));