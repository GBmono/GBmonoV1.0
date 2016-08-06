﻿/*
   header component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['categoryDataFactory', 'utilService', 'pluginService'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl(categoryDataFactory, utilService, pluginService) {
        var vm = this;
        // product top categories
        // devided into 4 collections map into 4 vertical list elements
        vm.topCatesCol1 = [];
        vm.topCatesCol2 = [];
        vm.topCatesCol3 = [];
        vm.topCatesCol4 = [];

        init();

        function init() {
            // get product top categories
            getProductTopCategories();

            // check if token exists
            isTokenExisted();

            // scroll
            window.addEventListener("scroll", function (event) {
                const pageTopEl = $("#pageTop");
                const headerDomEl = $("#menuHeader");
                var scrollTop = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
                if (scrollTop > 0) {
                    pageTopEl.show();
                    headerDomEl.addClass("min-header");
                }
                else {
                    headerDomEl.removeClass("min-header");
                    pageTopEl.hide();
                }
            });
        }

        function getProductTopCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    vm.topCatesCol1 = fillCategories(0, data);
                    vm.topCatesCol2 = fillCategories(4, data);
                    vm.topCatesCol3 = fillCategories(8, data);
                    vm.topCatesCol4 = fillCategories(12, data);
                    //console.log(vm.topCatesCol1);
                });
        }

        function isTokenExisted() {
            // check if token exists
            var token = utilService.getToken();
            // if no token
            if (!token || token === '') {
                // show login
                pluginService.showLogin();
            }
            else {
                // show profile
                pluginService.showProfile();
            }
        }

        function fillCategories(startIndex, data) {
            var result = [];
            for (var i = startIndex; i < startIndex + 4; i++) {
                if (i > data.length) {
                    break;
                }
                result.push(data[i]);
            }

            return result;
        }
    }

})(angular.module('gbmono'));

/*
   footer component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('footerController', ctrl);

    // controller body
    function ctrl() {

    }
})(angular.module('gbmono'));

/*
   ranking product list component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['productDataFactory'];

    // create controller
    module.controller('rankingController', ctrl);

    // controller body
    function ctrl(productDataFactory) {
        var vm = this;
        // products by ranking
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        init();

        function init() {
            // load products bu ranking
            getRankingProducts();
        }

        function getRankingProducts() {
            productDataFactory.getByRanking()
                .success(function (data) {
                    vm.products = data;
                });
        }
    }

})(angular.module('gbmono'));

/*
   profile component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    'utilService',
                    'pluginService',
                    'accountDataFactory'];

    // create controller
    module.controller('profileComponentController', ctrl);

    // controller body
    function ctrl($location,
                  utilService,
                  pluginService,
                  accountDataFactory) {
        var vm = this;
        // current user
        vm.user = {};

        // get the token from local storage
        var token = utilService.getToken();

        init();

        // view event handler
        vm.logout = function () {
            // clear token
            utilService.clearToken();
            // show login section on the header
            pluginService.showLogin();
            // redirect to home page
            $location.path('/login');
        };

        function init() {
            // if no token
            if (!token || token === '') {
                // redirect to home page
                $location.path('/login');
                return;
            }

            getUser(token);
        }

        // get the user by token
        function getUser(token) {
            // get the user by token
            accountDataFactory.getUser(token)
                .success(function (data) {
                    vm.user = data;
                })
                .error(function (error) {
                    // todo:
                    console.log('Failed to get the user.');
                    console.log(error);
                });
        }
        
    }
})(angular.module('gbmono'));