/*
    product list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', '$routeParams', 'productDataFactory', 'categoryDataFactory'];

    // create controller
    module.controller('productListController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, productDataFactory, categoryDataFactory) {
        // products
        $scope.products = [];
        // categories
        $scope.categories = [];
        // current category
        $scope.currentCategory = {};
        // category brands
        $scope.categoryBrands = [];

        // product image root path
        $scope.imgRoot = gbmono.img_root_path;
        // retreive category id from route params
        var categoryId = $routeParams.id ? parseInt($routeParams.id) : 0;
        
        init();

        function init() {
            if (categoryId != 0) {
                // load products
                getProducts(categoryId);

                // show up categories menu
                getCategories();

                // get category brands
                getCategoryBrands(categoryId);
            }
        }

        // get products by category
        function getProducts(categoryId) {
            productDataFactory.getByCategory(categoryId)
                .success(function (data) {
                    $scope.products = data;
                });
        }

        function getCategories() {            
            categoryDataFactory.getAll()
                .success(function (data) {
                    // reset categories
                    $scope.categories = [];
                    // reset the sub categories for the others for display purpose
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].categoryId != categoryId) {
                            data[i].subCategories = [];

                            // add into $scope
                            $scope.categories.push(data[i]);
                        }
                        else {
                            // we need make sure the current category with sub categories is on the top of the array
                            $scope.categories.unshift(data[i]);
                            // current category
                            $scope.currentCategory = data[i];
                        }
                    }
                });
        }

        function getCategoryBrands(categoryId) {
            categoryDataFactory.getBrands(categoryId)
                .success(function (data) {
                    $scope.categoryBrands = data;
                });
        }
    }
})(angular.module('gbmono'));
