/*
    login / register controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location', 'pluginService', 'utilService', 'accountDataFactory'];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl($location, pluginService, utilService, accountDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // login model
        vm.user = {};
        // register model
        vm.registeration = {};
        // loading 
        vm.dataLoading = false;

        init(); // page init 

        // event handlers
        vm.login = function () {
            vm.dataLoading = true;
            // sign in
            login(vm.user.userName, vm.user.password);
        };

        vm.register = function () {
            // check password validation
            if (vm.registeration.password != vm.registeration.confirmPassword) {
                alert('密码不一致!');
                return;
            }
            // we use email as default user name in gbmono
            vm.registeration.userName = vm.registeration.email;
            // register
            register(vm.registeration);
        };

        function init() {
            
        }

        function login(userName, password) {
            accountDataFactory.login(userName, password)
                .then(function successCallback(response) {
                    // save the bearer token into local storage                    
                    utilService.saveToken(response.data.access_token);
                    // save user name
                    utilService.saveUserName(response.data.userName); 
                    // direct into profile page
                    // todo: check return url??
                    $location.path('/profile');
                },
                function errorCallback(response) {
                    // show up error
                    alert('User name or password is incorrect.');
                    // pluginService.notify(response, 'error');
                    // set data loading to false
                    vm.dataLoading = false;
                });
        }

        function register(user) {
            accountDataFactory.register(user)
                .then(function successCallback(response) {
                    // auto sign-in
                    login(user.email, user.password);
                }, function errorCallback(response) {
                    // show up error
                    // pluginService.notify(response, 'error');
                    alert('Failed  to register.');
                    // set data loading to false
                    vm.dataLoading = false;
                });
        }
    }
})(angular.module('gbmono'));