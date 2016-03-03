/*
    product detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams', 'pluginService', 'productDataFactory', 'categoryDataFactory', 'accountDataFactory'];

    // create controller
    module.controller('productDetailController', ctrl);

    // controller body
    function ctrl($routeParams, pluginService, productDataFactory, categoryDataFactory, accountDataFactory) {
        //  use vm to represent the binding scope. This gets rid of the $scope variable from most of controllers
        var vm = this;
        // current product
        vm.product = {};
        // category menu
        vm.menu = {};
        // product images
        vm.productImages = [];
        // instruction images
        vm.instrutionImages = [];
        // extra info images
        vm.extraImages = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // if product is favorited
        vm.isFavorited = false;
        // retreive category id from route params
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // token
        // todo: change to angularJs local storage or cookie if browser doesn't suport local storage
        var token = window.localStorage.getItem(gbmono.LOCAL_STORAGE_TOKEN_KEY);

        // page init
        init();
        // event handlers
        vm.addFavorite = function () {
            var favorite = { productId: productId };
            
            addFavorite(token, favorite);
        };

        function init() {
            if (productId !== 0) {
                // get product by id
                getProduct(productId);
            }
        }

        // get product details by id
        function getProduct(productId) {
            productDataFactory.getById(productId)
                .success(function (data) {
                    // get current product
                    vm.product = data;
                    // filter image by type id
                    filterImages(data.images);
                    // get category menu by top category id
                    getCategoryMenu(vm.product.category.parentCategory.parentId);
                    // init img thumb gallery
                    pluginService.productDetailGallery(4000);
                    // init bootstrap tab
                    pluginService.tab();
                });
        }

        // get category menu
        // expanded current top category with subitems, and unselected collapsed menuitems
        function getCategoryMenu(topCateId) {
            categoryDataFactory.getMenu(topCateId)
                .success(function (data) {
                    vm.menu = data;
                });
        }

        // filter images by image type
        function filterImages(images) {
            for (var i = 0; i < images.length; i++) {
                // type id
                var imgTypeId = images[i].productImageTypeId
                // product img
                if (imgTypeId == gbmono.imgType.product) {
                    vm.productImages.push(images[i]);
                }
                else if (imgTypeId == gbmono.imgType.descritpion) {
                    // todo
                }
                else if (imgTypeId == gbmono.imgType.instruction) {
                    vm.instrutionImages.push(images[i]);
                }
                else if (imgTypeId == gbmono.imgType.extra) {
                    vm.extraImages.push(images[i]);
                }
            }
        }

        // add favorite
        function addFavorite(token, productId) {
            accountDataFactory.addFavorite(token, productId)
                .then(function successCallback(response) {
                    vm.isFavorited = true;
                    
                }, function errorCallback(response) {
                    console.log(response);
                    // if user is not authencited
                    if (response.status === 401) {
                        // direct into login page
                        // todo: returnUrl
                        $location.path('/login');
                    }
                });
        }

        // remove favorite
        function removeFavorite(token, productId) {
            accountDataFactory.removeFavorite(token, productId)
                .then(function successCallback(response) {


                }, function errorCallback(response) {
                    // if user is not authencited
                    if (response.status === 401) {
                        // direct into login page
                        // todo: returnUrl
                        $location.path('/login');
                    }
                });
        }
    }
})(angular.module('gbmono'));