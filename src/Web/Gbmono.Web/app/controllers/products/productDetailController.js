/*
    product detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$location',
                    'pluginService',
                    'utilService',
                    'productDataFactory',
                    'categoryDataFactory',
                    'accountDataFactory',
                    'userFavoriteDataFactory'];

    // create controller
    module.controller('productDetailController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $location,
                  pluginService,
                  utilService,
                  productDataFactory,
                  categoryDataFactory,
                  accountDataFactory,
                  userFavoriteDataFactory) {
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
        // visited products
        vm.visitedProducts = [];
        // loading image
        // this staging loading img would be replaced by actual product img once the data is loaded
        vm.primaryImg = 'loading_2.gif';
        // no image
        vm.noImage = 'No_Image.png';
        // retreive category id from route params
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // get token from local storage
        var token = utilService.getToken();
        // user favorite model
        var userFavorite = { saveItemType: 1, keyId: productId }; // product: save item type id = 1

        // page init
        init();

        // event handlers
        // save product into user favorites
        vm.saveProduct = function () {
            // user is not authenticated if toke doesn't exist
            if (!token || token == '') {
                // redirect into login page with return url
                utilService.redirectToLoginPage('products', productId);                
            }

            // save
            addFavorite(token, userFavorite);
        };

        // remove product from user favorites
        vm.removeProduct = function () {
            // remove
            removeFavorite(token, userFavorite);
        };

        function init() {
            // move into the top of the screen to focus on the product detail area
            // utilService.scrollToTop();

            // when product id is invalid
            if (productId == 0) {
                // todo:
            }

            // get product by id
            getProduct(productId, token);
            
            // if product is favorited
            isFavoritedProduct(token, userFavorite);

            // get visited products
            getVisitedProducts();
        }

        // get product details by id
        function getProduct(productId, authToken) {
            productDataFactory.getById(productId, authToken)
                .success(function (data) {
                    // get current product
                    vm.product = data;
                    // filter image by type id
                    filterImages(data.images);
                    // replace the loading img by actual product image
                    // handle no images returned
                    vm.primaryImg = vm.productImages.length > 0 ? vm.productImages[0].fileName : vm.noImage;
                    // ui effects
                    // init img thumb gallery
                    pluginService.productDetailGallery();
                    // init bootstrap tab
                    pluginService.tab();
                    // save into visted product
                    saveVisitedProduct(vm.product);
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

        // check if product is favorited
        function isFavoritedProduct(token, model) {
            // user is not authenticated if toke doesn't exist
            if (!token || token == '') {
                vm.isFavorited = false;
                return;
            }

            userFavoriteDataFactory.isSaved(token, model)
                .success(function (status) {
                    if (status) {
                        vm.isFavorited = true;
                    }
                    else {
                        vm.isFavorited = false;
                    }
                })
                .error(function (error) {
                    // ignore the 401 error
                });
        }

        // add favorite
        function addFavorite(token, favorite) {
            userFavoriteDataFactory.save(token, favorite)
                .then(function successCallback(response) {
                    vm.isFavorited = true;
                    
                }, function errorCallback(response) {                    
                    // if user is not authencited
                    if (response.status === 401) {
                        // direct into login page
                        // redirect into login page with return url
                        utilService.redirectToLoginPage('products', productId);
                    }
                });
        }

        // remove favorite
        function removeFavorite(token, model) {
            userFavoriteDataFactory.remove(token, model.saveItemType, model.keyId)
                .then(function successCallback(response) {
                    vm.isFavorited = false;

                }, function errorCallback(response) {
                    // if user is not authencited
                    if (response.status === 401) {
                        // redirect into login page with return url
                        utilService.redirectToLoginPage('products', productId);
                    }
                });
        }
        
        // get visited products
        function getVisitedProducts() {
            // todo: exclude the current product??
            vm.visitedProducts = utilService.getVisitedProducts();
        }

        // save current product into visited products
        function saveVisitedProduct(product) {
            var newVisitedProduct = {
                productId: product.productId,
                productName: product.primaryName,
                brandName: product.brand.name,
                imgUrl: vm.imgRoot + vm.primaryImg,
                price: product.price
            };

            // save into list
            utilService.saveVisitProduct(newVisitedProduct);
        }
    }
})(angular.module('gbmono'));