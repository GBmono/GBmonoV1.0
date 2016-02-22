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
    ctrl.$inject = ['$scope', 'categoryDataFactory'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl($scope, categoryDataFactory) {
        // top categories
        // $scope.topCates = [];

        // todo: to use filter instead
        $scope.topCatesCol1 = [];
        $scope.topCatesCol2 = [];
        $scope.topCatesCol3 = [];
        $scope.topCatesCol4 = [];

        // page init 
        init();

        function init() {
            getTopCategories();
        }

        function getTopCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    $scope.topCatesCol1 = fillCategories(0, data);
                    $scope.topCatesCol2 = fillCategories(4, data);
                    $scope.topCatesCol3 = fillCategories(8, data);
                    $scope.topCatesCol4 = fillCategories(12, data);
                });
        }

        function fillCategories(startIndex, data) {
            var result = [];
            for (var i = startIndex; i < startIndex + 4; i++) {
                result.push(data[i]);
            }

            return result;
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





