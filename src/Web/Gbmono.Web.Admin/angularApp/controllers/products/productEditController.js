/*
    product edit page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    '$filter',
                    '$routeParams',
                    'categoryDataFactory',
                    'productDataFactory',
                    'productImageDataFactory',
                    'brandDataFactory',
                    'countryDataFactory',
                    'pluginService',
                    'validator'];

    // create controller
    module.controller('productEditController', ctrl);

    // controller body
    function ctrl($scope,
                  $filter,
                  $routeParams,
                  categoryDataFactory,
                  productDataFactory,
                  productImageDataFactory,
                  brandDataFactory,
                  countryDataFactory,
                  pluginService,
                  validator) {
        // top level categories
        $scope.topCates = [];
        $scope.selectedTopCateId = 0;

        // second level categories
        $scope.secondCates = [];
        $scope.selectedSecondCateId = 0;

        // third level categories
        $scope.thirdCates = [];

        // brands collection
        $scope.brands = [];

        // country collection
        $scope.countries = [];

        // edit product model
        $scope.editProduct = {};

        // image url root
        $scope.imgUrl = gbmono.img_root_path;
        // product images
        $scope.images = [];
        // image type
        $scope.selectedImgTypeId = "1";

        // retreive product id from routeparams
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;

        init();

        /* event handlers from view */
        // top cate changed
        $scope.topCateChanged = function () {
            // reload second cates and third cates
            getSecondCates($scope.selectedTopCateId);
        };

        // second cate changed
        $scope.secondCateChanged = function () {
            // reload third cates
            getThirdCates($scope.selectedSecondCateId);
        };

        // file upload on select, check file extension. only jpg, png allowed
        $scope.onSelect = function (e) {
            $.each(e.files, function (index, value) {
                if (value.extension.toLowerCase() !== '.jpg' && value.extension.toLowerCase() !== '.png') {
                    e.preventDefault();
                    alert("Only .jpg / .png allowed.");
                }
            });
        };

        // file upload on success 
        $scope.onSuccess = function () {
            // upload image succeed
            // then display the image in view
            getImages($scope.editProduct.productId);
        };

        // page init
        init();

        function init() {
            // image upload target url
            $scope.fileUploadUrl = gbmono.api_site_prefix.product_image_api_url + '/Upload/' + productId + "/" + $scope.selectedImgTypeId;
            
            // get product
            getProduct(productId);

            // get product images
            getImages(productId);
        }

        function getProduct(id) {
            productDataFactory.getById(id)
                .success(function (data) {
                    $scope.editProduct = data;

                    // format the activation date json into date format
                    $scope.editProduct.activationDate = $filter('date')($scope.editProduct.activationDate, 'yyyy-MM-dd')
                    // load categories
                    // select category by product category
                    // get top categories and auto load second, third level categories
                    getTopCategories();

                    // get brands
                    getBrands();

                    // get countries
                    getCountries();
                });
        }

        function getImages(id) {
            productImageDataFactory.getByProduct(id)
                .success(function (data) {
                    $scope.images = data;
                });
        }

        function getTopCategories() {
            categoryDataFactory.getTopCategories()
                .success(function (data) {
                    $scope.topCates = data;
                    // default selection 
                    $scope.selectedTopCateId = $scope.editProduct.category.parentCategory.parentId;
                    // auto load second level categories
                    getSecondCates($scope.selectedTopCateId)
                });
        }

        function getSecondCates(topCateId) {
            categoryDataFactory.getByParent(topCateId)
                .success(function (data) {
                    $scope.secondCates = data;
                    // default selected id
                    $scope.selectedSecondCateId = $scope.editProduct.category.parentId;
                    // auto load third level categories
                    getThirdCates($scope.selectedSecondCateId);
                });
        }

        function getThirdCates(secondCateId) {
            categoryDataFactory.getByParent(secondCateId)
                .success(function (data) {
                    $scope.thirdCates = data;
                    
                });
        }

        function getBrands() {
            brandDataFactory.getAll()
                .success(function (data) {
                    $scope.brands = data;
                });
        }

        function getCountries() {
            countryDataFactory.getAll()
                .success(function (data) {
                    $scope.countries = data;
                });
        }

        function update(product) {
            productDataFactory.update(product)
                .success(function () {
                    pluginService.notify('产品更新成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
  
    }
})(angular.module('gbmono'));