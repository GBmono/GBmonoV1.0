/*
    master (layout) controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$timeout'];

    // create controller
    module.controller('masterController', ctrl);

    // controller body
    function ctrl($timeout) {
        var vm = this;

        // back to top
        vm.backToTop = function () {
            $("html,body").animate({
                scrollTop: 0
            }, 300);

            return false;
        };

        init(); // page init

        function init() {
            calcHeight();
        }

        // calc the min height of the content area
        function calcHeight() {            
            var windowHeight = $(window).height();
            $(".container").css("min-height", windowHeight - 254);
        }
    }
})(angular.module('gbmono'));