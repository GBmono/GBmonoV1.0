/*
    master (layout) controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('masterController', ctrl);

    // controller body
    function ctrl() {
        var vm = this;

        // back to top
        vm.backToTop = function () {
            $("html,body").animate({
                scrollTop: 0
            }, 300);

            return false;
        };
    }
})(angular.module('gbmono'));