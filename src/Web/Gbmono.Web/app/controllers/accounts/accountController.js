/*
    login / register controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    'pluginService',
                    'utilService',
                    'accountDataFactory'];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl($location,
                  pluginService,
                  utilService,
                  accountDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // login model
        vm.user = {};
        // register model
        vm.registeration = {};
        // loading 
        vm.dataLoading = false;
        // error message
        vm.isLoginFailed = false;
        vm.loginError = '';
        vm.isRegisterFailed = false;
        vm.registerError = '';

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
                // set flag
                vm.isRegisterFailed = true;
                // show error
                vm.registerError = '密码不一致.';
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
            // reset flag
            vm.isLoginFailed = false;

            // authenticate user name and password
            accountDataFactory.login(userName, password)
                .then(function successCallback(response) {
                    // save the bearer token into local storage                    
                    utilService.saveToken(response.data.access_token);
                    // show profile on the header
                    pluginService.showProfile();
                    // save user name
                    utilService.saveUserName(response.data.userName);
                    // redirect to previouse page or profile page
                    utilService.redirectBack(); 
                },
                function errorCallback(response) {
                    // set flag to true
                    vm.isLoginFailed = true;
                    // show up error
                    vm.loginError = extractErrorMessage(response);
                    // set data loading to false
                    vm.dataLoading = false;
                    // show login on the header
                    pluginService.showLogin();
                });
        }

        function register(user) {
            // reset flag
            vm.isRegisterFailed = false;

            // register new user
            accountDataFactory.register(user)
                .then(function successCallback(response) {
                    // auto sign-in
                    login(user.email, user.password);
                }, function errorCallback(response) {
                    // set flag
                    vm.isRegisterFailed = true;
                    // show error
                    vm.registerError = response.data;

                    // set data loading to false
                    vm.dataLoading = false;
                });
        }


        // extract error message from response
        function extractErrorMessage(response) {
            var msg = '';
            if (response.data) {
                if (response.data.message) {
                    msg = response.data.message;
                } else if (response.data.error_description) {
                    msg = response.data.error_description;
                }

            } else {
                // generic error message
                msg = 'Unexpected error has occurred. Please contact the administrator.';
            }

            return msg;
        }
    }
})(angular.module('gbmono'));