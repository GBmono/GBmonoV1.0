/*
    product create page controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope',
                    'categoryDataFactory',
                    'productDataFactory',
                    'brandDataFactory',
                    'countryDataFactory',
                    'pluginService',
                    'validator'];

    // create controller
    module.controller('productCreateController', ctrl);

    // controller body
    function ctrl($scope,
                  categoryDataFactory,
                  productDataFactory,
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

        // new product model
        $scope.newProduct = {};

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
        $scope.create = function () {
            // console.log($scope.newProduct);
            // validation
            if (!validator.isValidProduct($scope.newProduct)) {
                pluginService.notify('数据格式错误', 'error')
                return;
            }

            //productDataFactory.getByCategory(22)
            //    .success(function (data) {
            //        alert('success');
            //    });

            // create product
            productDataFactory.create($scope.newProduct)
                .success(function () {
                    //pluginService.notify('Product is created', 'success')
                })
                .error(function (error) {
                    //pluginService.notify(error, 'error');
                })

        };

        function init() {
            // get top categories and auto load second, third level categories
            getTopCategories();

            // get brands
            getBrands();

            // get countries
            getCountries();
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
                    $scope.newProduct.categoryId = $scope.thirdCates.length > 0 ? $scope.thirdCates[0].categoryId : 0;
                });
        }

        function getBrands() {
            brandDataFactory.getAll()
                .success(function (data) {
                    $scope.brands = data;
                    // default selection
                    $scope.newProduct.brandId = $scope.brands.length > 0 ? $scope.brands[0].brandId : 0;
                });
        }

        function getCountries() {
            countryDataFactory.getAll()
                .success(function (data) {
                    $scope.countries = data;
                    // default seleciton
                    $scope.newProduct.countryId = $scope.countries.length > 0 ? $scope.countries[0].countryId : 0;
                });
        }

        function createProduct(product) {
            productDataFactory.create(product)
                .success(function (data) {
                    pluginService.notify('产品添加成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }
    }
})(angular.module('gbmono'));


/*
   product image create controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', '$routeParams'];

    // create controller
    module.controller('productImageCreateController', ctrl);

    // controller body
    function ctrl($scope, $routeParams) {
        // image upload target url
        $scope.fileUploadUrl = gbmono.api_site_prefix.product_image_api_url + '/Upload';

        // product images
        $scope.images = [];

        // event handlers
        // check file extension. only jpg, png allowed
        $scope.onSelect = function (e) {
            $.each(e.files, function (index, value) {
                if (value.extension.toLowerCase() !== '.jpg' && value.extension.toLowerCase() !== '.png') {
                    e.preventDefault();
                    alert("Only .jpg / .png allowed.");
                }
            });
        };

        // on success 
        $scope.onSuccess = function () {
            // upload image succeed
            // then display the image in view
        };

    }
})(angular.module('gbmono'));


/*
   product shop create controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('productShopCreateController', ctrl);

    // controller body
    function ctrl() {

    }
})(angular.module('gbmono'));