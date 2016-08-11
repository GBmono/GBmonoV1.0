/*
   ranking product list component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['productDataFactory'];

    // create controller
    module.controller('rankingController', ctrl);

    // controller body
    function ctrl(productDataFactory) {
        var vm = this;
        // products by ranking
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        init();

        function init() {
            // load products bu ranking
            getRankingProducts();
        }

        function getRankingProducts() {
            productDataFactory.getByRanking()
                .success(function (data) {
                    vm.products = data;
                });
        }
    }

})(angular.module('gbmono'));

/*
   profile component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$location',
                    'utilService',
                    'pluginService',
                    'accountDataFactory'];

    // create controller
    module.controller('profileComponentController', ctrl);

    // controller body
    function ctrl($scope,
                  $location,
                  utilService,
                  pluginService,
                  accountDataFactory) {
        var vm = this;
        // current user
        vm.user = {};

        // get the token from local storage
        var token = utilService.getToken();

        init();

        // view event handler
        vm.logout = function () {
            // clear token
            utilService.clearToken();
            // show login section on the header
            // access variable in parent controller (master controller with notation 'masterViewModel')
            $scope.masterViewModel.isAuthenticated = false;

            // redirect to home page
            $location.path('/login');
        };

        function init() {
            // if no token
            if (!token || token === '') {
                // redirect to home page
                $location.path('/login');
                return;
            }

            getUser(token);
        }

        // get the user by token
        function getUser(token) {
            // get the user by token
            accountDataFactory.getUser(token)
                .success(function (data) {
                    vm.user = data;
                })
                .error(function (error) {
                    // todo:
                    console.log('Failed to get the user.');
                    console.log(error);
                });
        }
        
    }
})(angular.module('gbmono'));