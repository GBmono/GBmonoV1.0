/*
    home controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', 'pluginService'];

    // create controller
    module.controller('homeController', ctrl);

    // controller body
    function ctrl($scope, pluginService) {
        init();

        function init() {
            pluginService.slider();
        }
    }
})(angular.module('gbmono'));
