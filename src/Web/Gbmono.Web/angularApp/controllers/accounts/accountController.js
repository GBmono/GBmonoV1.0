/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('profileController', ctrl);

    // controller body
    function ctrl() {
        var vm = this;
        vm.title = '个人中心';

        init(); // page init 

        function init() {

        }
    }
})(angular.module('gbmono'));

/*
    login / register controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl() {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;

        init(); // page init 

        function init() {

        }
    }
})(angular.module('gbmono'));