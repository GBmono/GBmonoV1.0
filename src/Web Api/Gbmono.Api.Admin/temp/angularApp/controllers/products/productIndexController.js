/*
    product index page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'categoryDataFactory'];

    // create controller
    module.controller('productIndexController', ctrl);

    // controller body
    function ctrl($scope, categoryDataFactory) {
        // top level categories
        $scope.topCates = [];
        $scope.selectedTopCateId = 0;

        // second level categories
        $scope.secondCates = [];
        $scope.selectedSecondCateId = 0;

        // third level categories
        $scope.thirdCates = [];
        $scope.selectedThirdCateId = 0;

        init();

        /* event handlers from view */
        $scope.topCateChanged = function () {
            // reload second cates and third cates
            getSecondCates($scope.selectedTopCateId);
        };

        $scope.secondCateChanged = function () {
            // reload third cates
            getThirdCates($scope.selectedSecondCateId);
        };


        function init() {
            // get top categories and auto load second, third level categories
            getTopCategories();
        }

        function getTopCategories() {
            categoryDataFactory.getTopCategories()
                .success(function (data) {
                    $scope.topCates = data;
                    // default selection 
                    $scope.selectedTopCateId = $scope.topCates.length > 0 ? $scope.topCates[0].categoryId : 0;
                    // auto load second level categories
                    getSecondCates($scope.selectedTopCateId)
                });
        }

        function getSecondCates(topCateId) {
            categoryDataFactory.getByParent(topCateId)
                .success(function (data) {
                    $scope.secondCates = data;
                    // default selected id
                    $scope.selectedSecondCateId = $scope.secondCates.length > 0 ? $scope.secondCates[0].categoryId : 0;
                    // auto load third level categories
                    getThirdCates($scope.selectedSecondCateId);
                });
        }

        function getThirdCates(secondCateId) {
            categoryDataFactory.getByParent(secondCateId)
                .success(function (data) {
                    $scope.thirdCates = data;
                    // default selected id
                    $scope.selectedThirdCateId = $scope.thirdCates.length > 0 ? $scope.thirdCates[0].categoryId : 0;
                });
        }
    }
})(angular.module('gbmono'));



