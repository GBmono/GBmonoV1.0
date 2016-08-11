/*
    master (layout) controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$timeout',
                    'categoryDataFactory',
                    'utilService',
                    'pluginService'];

    // create controller
    module.controller('masterController', ctrl);

    // controller body
    function ctrl($timeout,
                  categoryDataFactory,
                  utilService,
                  pluginService) {
        var vm = this;
        // product top categories
        // devided into 4 collections map into 4 vertical list elements
        vm.topCatesCol1 = [];
        vm.topCatesCol2 = [];
        vm.topCatesCol3 = [];
        vm.topCatesCol4 = [];
        // if user is authenticated
        // this variable will be access from child controller
        // the value is updated when user pass or fail the authentication
        vm.isAuthenticated = false;

        // back to top
        vm.backToTop = function () {
            $("html,body").animate({
                scrollTop: 0
            }, 300);

            return false;
        };

        init(); // page init

        function init() {
            // get product top categories
            getProductTopCategories();

            // populate min height of content area
            calcHeight();
            
            // check if token exists
            isTokenExisted();
        }

        // get product top categories
        function getProductTopCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    vm.topCatesCol1 = fillCategories(0, data);
                    vm.topCatesCol2 = fillCategories(4, data);
                    vm.topCatesCol3 = fillCategories(8, data);
                    vm.topCatesCol4 = fillCategories(12, data);
                });
        }

        function isTokenExisted() {
            // check if token exists
            var token = utilService.getToken();
            // if no token
            if (!token || token === '') {
                // show login
                vm.isAuthenticated = false;
            }
            else {
                // show profile
                vm.isAuthenticated = true;
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

        // calc the min height of the content area
        function calcHeight() {            
            var windowHeight = $(window).height();
            $(".container").css("min-height", windowHeight - 160);
        }
    }
})(angular.module('gbmono'));