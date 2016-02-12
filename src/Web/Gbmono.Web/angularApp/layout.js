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
   header block controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', 'pageDataFactory'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl($scope, pageDataFactory) {
        // menu data
        $scope.menuItems = [];

        // page init 
        init();

        function init() {
            // load menu data from json file
            getMenuData();
        }

        function getMenuData() {
            pageDataFactory.getMenuData()
                .success(function (data) {
                    $scope.menuItems = data;
                    //$scope.menuItems = data;
                    // console.log(vm);
                });
        }
    }

})(angular.module('gbmono'));


/*
   footer block controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', 'pageDataFactory'];

    // create controller
    module.controller('footerController', ctrl);

    // controller body
    function ctrl($scope, pageDataFactory) {
        $scope.footerDataItems = [];

        // page init
        init();

        function init() {
            // load menu data from json file
            getFooterData();
        }

        function getFooterData() {
            pageDataFactory.getFooterData()
                .success(function (data) {
                    $scope.footerDataItems = data;
                });
        }
    }
})(angular.module('gbmono'));





