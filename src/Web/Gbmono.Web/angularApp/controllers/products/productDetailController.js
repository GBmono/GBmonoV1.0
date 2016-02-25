/*
    product detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', '$routeParams', 'pluginService', 'productDataFactory', 'categoryDataFactory'];

    // create controller
    module.controller('productDetailController', ctrl);

    // controller body
    function ctrl($scope, $routeParams, pluginService, productDataFactory, categoryDataFactory) {
        // current product
        $scope.product = {};
        // category list with expanded current category
        $scope.categories = [];

        // product image root path
        $scope.imgRoot = gbmono.img_root_path;
        // retreive category id from route params
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;

        init();

        function init() {
            if (productId !== 0) {
                // get product by id
                getProduct(productId);
            }            
        }

        function getProduct(productId) {
            productDataFactory.getById(productId)
                .success(function (data) {
                    // get current product
                    $scope.product = data;
                    // load categories with expanded category id (top level category)
                    getCategories($scope.product.category.parentCategory.parentId)
                    // init img thumb gallery
                    pluginService.productDetailGallery(4000);
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
                        }
                    }
                });
        }

    }
})(angular.module('gbmono'));