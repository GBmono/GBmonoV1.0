/*
    product list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', '$routeParams', '$location', 'productDataFactory', 'categoryDataFactory'];

    // create controller
    module.controller('productListController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, $location, productDataFactory, categoryDataFactory) {
        // products
        $scope.products = [];
        // categories
        $scope.categories = [];
        //  third cateogories (tags in filter)
        $scope.thirdCategories = [];
        // current category
        $scope.currentCategory = {};
        // current second category (optional)
        $scope.currentSubCategory = {};

        // category brands
        $scope.categoryBrands = [];

        // product image root path
        $scope.imgRoot = gbmono.img_root_path;
        // retreive top category id from route params
        var topCategoryId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // sub category id (optional)
        var subCateId = $location.search().subcateid ? parseInt($location.search().subcateid) : 0;
    
        init();

        function init() {
            $scope.subCateId = subCateId;
            if (topCategoryId != 0) {
                // load products by top category or sub category
                getProducts(subCateId == 0 ? topCategoryId : subCateId);

                // category menu list with expanded top category
                getCategories(topCategoryId);

                // third categories in filter tag
                getThirdCategories(subCateId == 0 ? topCategoryId : subCateId);

                // get category brands
                getCategoryBrands(subCateId == 0 ? topCategoryId : subCateId);
            }
        }

        // get products by category
        function getProducts(categoryId) {
            productDataFactory.getByCategory(categoryId)
                .success(function (data) {
                    $scope.products = data;
                });
        }

        function getCategories(expandadCategoryId) {
            categoryDataFactory.getAll()
                .success(function (data) {
                    // reset categories
                    $scope.categories = [];
                    // reset the sub categories for the others for display purpose
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].categoryId != expandadCategoryId) {
                            data[i].subCategories = [];

                            // add into $scope
                            $scope.categories.push(data[i]);
                        }
                        else {
                            // we need make sure the current category with sub categories is on the top of the array
                            $scope.categories.unshift(data[i]);
                            // current category
                            $scope.currentCategory = data[i];

                            // subcategory selected (optional)
                            if (subCateId !== 0) {                                
                                for (var j = 0; j < $scope.currentCategory.subCategories.length; j++) {
                                    if ($scope.currentCategory.subCategories[j].categoryId == subCateId) {
                                        $scope.currentSubCategory = $scope.currentCategory.subCategories[j];
                                    }
                                }
                            }
                        }
                    }
                });
        }

        function getThirdCategories(categoryId) {
            categoryDataFactory.getThirdCates(categoryId)
                .success(function (data) {
                    $scope.thirdCategories = data;
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
