/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    '$timeout',
                    'utilService',
                    'pluginService',
                    'userFavoriteDataFactory'];

    // create controller
    module.controller('profileController', ctrl);

    // controller body
    function ctrl($location,
                  $timeout,
                  utilService,
                  pluginService,
                  userFavoriteDataFactory) {
        // controller 
        var vm = this;
        // user saved products
        vm.products = [];
        // todo: user saved articles

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
        
        // view event handler
        vm.logout = function () {
            // clear token
            utilService.clearToken();
            // redirect into login page or home page??
            $location.path('/login');
        };

        init(); // page init 

        function init() {
            // if no token
            if (!token || token === '') {
                // redirect into login page
                $location.path('/login');
            }
            // init tab
            initTab();

            // get current user name
            vm.name = utilService.getUserName();
           
            // get user favorites
            getFavoriteProducts(token, vm.paging.pageIndex, vm.paging.pageSize);
        }

        function getFavoriteProducts(userToken, pageIndex, pageSize) {
            // loading icon
            pluginService.showDataLoadingIndicator('#products', { left: "50%", top: "80px;" });

            // call service
            userFavoriteDataFactory.getFavoriteProducts(userToken, pageIndex, pageSize)
                .then(function successCallback(response) {
                    // add products into collection
                    vm.products = vm.products.concat(response.data);
                    // add page index
                    vm.paging.pageIndex++;
                    // if no more products 
                    if (response.data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                    }
                   
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#products');

                }, function errorCallback(response) {
                    // if user is not authorized
                    if (response.status === 401) {
                        // direct into login page
                        // todo: returnUrl
                        $location.path('/login');
                    }
                });
        }

        function initTab() {
            $timeout(function () {
                $(".nav>ul>li").on("click", function (e) {
                    var currentBtn = $(e.currentTarget);
                    if (currentBtn.hasClass("selected")) {
                        return false;
                    }

                    var chooseItem = currentBtn.attr("chooseitem");


                    $(".news-list").removeClass("selected");
                    switch (chooseItem) {
                        case "product":
                            $(".news-list.product").addClass("selected");
                            break;
                        case "article":
                            $(".news-list.article").addClass("selected");
                            break;
                    }

                    $(".nav>ul>li").removeClass("selected");
                    currentBtn.addClass("selected");

                });

            });
        }
    }
})(angular.module('gbmono'));