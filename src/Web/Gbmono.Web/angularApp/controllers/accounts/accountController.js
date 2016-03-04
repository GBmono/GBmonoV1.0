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
            login(vm.user);
        };

        function init() {

        }

        function login(user) {
            accountDataFactory.login(user.userName, user.password)
                .then(function successCallback(response) {
                    // todo: save token into local storage or cookie
                    // save the bearer token into local storage                    
                    utilService.saveToken(response.data.access_token);
                    // window.localStorage.setItem(gbmono.LOCAL_STORAGE_TOKEN_KEY, response.data.access_token);
                    // window.localStorage.setItem(gbmono.LOCAL_STORAGE_USERNAME_KEY, response.data.userName);
                    
                    // direct into profile page
                    // $location.path('/profile');
                },
                function errorCallback(response) {
                    pluginService.notify(response, 'error');
                    // var errorMessage = getErrorResult(response);
                    // console.log(response);
                    // alert(errorMessage);
                    vm.dataLoading = false;
                });
        }

        function getErrorResult(errorResponse) {
            if (errorResponse.data && errorResponse.data.error_description) {
                return errorResponse.data.error_description;
            }

            // unknown error
            return errorResponse.statusText;
        }
    }
})(angular.module('gbmono'));