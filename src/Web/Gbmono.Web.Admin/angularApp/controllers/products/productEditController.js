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
                    'productTagDataFactory',
                    'brandDataFactory',
                    'pluginService',
                    'validator',
                    'utilService'];

    // create controller
    module.controller('productEditController', ctrl);

    // controller body
    function ctrl($scope,
                  $filter,
                  $routeParams,
                  categoryDataFactory,
                  productDataFactory,
                  productImageDataFactory,
                  productTagDataFactory,
                  brandDataFactory,
                  pluginService,
                  validator,
                  utilService) {
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
        $scope.imgUrl = gbmono.img_product_path;
        // product images
        $scope.images = [];
        // image type
        $scope.selectedImgTypeId = "1";
        // editing image
        $scope.editImage = {};
        // img types
        $scope.imgTypes = [{ name: '商品图片', value: 1 }, { name: '介绍图片', value: 2 }, { name: "使用说明图片", value: 3 }, { name: "追加文案图片", value: 4 }];

        // tags
        $scope.tags = [];


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

        // update the saveUrl when img type is changed
        $scope.imgTypeChanged = function () {
            $("#files").data("kendoUpload").options.async.saveUrl = gbmono.api_site_prefix.product_image_api_url + '/Upload/' + productId + "/" + $scope.selectedImgTypeId;
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

        // attach authorization token before uploading file
        $scope.onUpload = function (e) {
            // get httl request obj
            var xhr = e.XMLHttpRequest;

            // get token from local storage
            var token = utilService.getToken();

            // attach token 
            if (xhr) {
                xhr.addEventListener("readystatechange", function (e) {
                    if (xhr.readyState == 1 /* OPENED */) {
                        xhr.setRequestHeader("Authorization", "Bearer " + token);
                    }
                });
            }
        };

        // file upload on success 
        $scope.onSuccess = function () {
            // upload image succeed
            // then display the image in view
            getImages($scope.editProduct.productId);
        };

        // update product 
        $scope.update = function () {
            update($scope.editProduct);
        };

        // open edit product image window
        $scope.openEditImgWin = function (image) {
            $scope.editImage = image;
            // open kendo window
            $scope.winEdit.open();

        };

        // update product img info
        $scope.updateImg = function () {
            updateImageInfo($scope.editImage);
        };

        // remove product image
        $scope.delete = function (id) {
            removeImage(id);
        };

        // helper: convert img type id into text
        $scope.showTypeName = function (typeId) {
            if (typeId === 1) {
                return '商品图片';
            }
            else if (typeId === 2) {
                return '介绍图片';
            }
            else if (typeId === 3) {
                return '使用说明图片';
            }
            else {
                return '追加文案图片';
            }
        }

        // page init
        init();

        function init() {
            // image upload target url
            $scope.fileUploadUrl = gbmono.api_site_prefix.product_image_api_url + '/Upload/' + productId + "/" + $scope.selectedImgTypeId;

            // get product
            getProduct(productId);

            // get product images
            getImages(productId);


            // get product images
            //getTags(productId);
        }

        function getProduct(id) {
            productDataFactory.getById(id)
                .success(function (data) {
                    $scope.editProduct = data;

                    // format the activation date json into date format
                    $scope.editProduct.activationDate = $filter('date')($scope.editProduct.activationDate, 'yyyy-MM-dd')

                    // select category by product category
                    // get top categories and auto load second, third level categories
                    getTopCategories();

                    // get brands
                    getBrands();

                    // generate barcode image
                    generateBarcodeImage($scope.editProduct.barCode);

                });
        }

        function getImages(id) {
            productImageDataFactory.getByProduct(id)
                .success(function (data) {
                    $scope.images = data;
                });
        }

        function getTags(id) {
            productTagDataFactory.getByProductId(id)
              .success(function (data) {
                  $scope.tags = data;
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

        function update(product) {
            productDataFactory.update(product)
                .success(function () {
                    pluginService.notify('产品更新成功', 'success');
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }

        function updateImageInfo(image) {
            productImageDataFactory.update(image)
                .success(function () {
                    // reload images
                    getImages($scope.editProduct.productId);

                    // close window
                    $scope.winEdit.close();
                })
                .error(function (error) {
                    pluginService.notify(error, 'error')
                });
        }

        function removeImage(id) {
            // warning text
            var r = confirm(gbmono.notification.delText);
            if (r) {
                productImageDataFactory.remove(id)
                    .success(function () {
                        // reload images
                        getImages($scope.editProduct.productId);
                    })
                    .error(function (error) {
                        pluginService.notify(error, 'error')
                    });
            }
        }

        function generateBarcodeImage(barcode) {
            pluginService.generateBarcodeImage('#barcodeImg', barcode, 'ean13');
        }
    }
})(angular.module('gbmono'));