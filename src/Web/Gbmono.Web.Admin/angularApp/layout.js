/*
    layout controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('layoutController', ctrl);

    // controller body
    function ctrl() {

    }
})(angular.module('gbmono'));

/*
    header controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl($scope) {
        // toggle right side bar
        $scope.toggleSidebar = function () {
            $('#rightSidebar').toggleClass('toggled');
        };
    }
})(angular.module('gbmono'));






