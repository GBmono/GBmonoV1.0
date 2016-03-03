/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location', 'accountDataFactory'];

    // create controller
    module.controller('profileController', ctrl);

    // controller body
    function ctrl($location, accountDataFactory) {
        var vm = this;
        // user favorites
        vm.favorites = [];
        // user object
        // todo: change to angularJs local storage or cookie if browser doesn't suport local storage
        var token = window.localStorage.getItem(gbmono.LOCAL_STORAGE_TOKEN_KEY);
        // console.log(token);

        init(); // page init 
        
        function init() {
            // get user favorites
            getUserFavorites(token);
        }

        // user then promise to get detailed error response
        function getUserFavorites(token) {
            accountDataFactory.getFavorites(token)
                .then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    vm.favorites = response.data;
                }, function errorCallback(response) {
                   
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    if (response.status === 401) {
                        // direct into login page
                        $location.path('/login');
                    }
                });
        }
    }
})(angular.module('gbmono'));

/*
    login / register controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location', 'accountDataFactory'];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl($location, accountDataFactory) {
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
                    window.localStorage.setItem(gbmono.LOCAL_STORAGE_TOKEN_KEY, response.data.access_token);
                    window.localStorage.setItem(gbmono.LOCAL_STORAGE_USERNAME_KEY, response.data.userName);
                    
                    // direct into profile page
                    // $location.path('/profile');
                },
                function errorCallback(response) {
                    var errorMessage = getErrorResult(response);
                    console.log(response);
                    alert(errorMessage);
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