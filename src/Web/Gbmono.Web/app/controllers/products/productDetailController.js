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
        // loading image
        // this staging loading img would be replaced by actual product img once the data is loaded
        vm.primaryImg = 'loading_2.gif';
        // no image
        vm.noImage = 'No_Image.png';
        // retreive category id from route params
        var productId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // get token from local storage
        var token = utilService.getToken();

        // page init
        init();
        // event handlers
        vm.toggleFavorite = function () {
            if (vm.isFavorited) {
                // remove
                removeFavorite(token, productId);
            }
            else {
                // add 
                var favorite = { productId: productId };
                // add into user favorite
                addFavorite(token, favorite);
            }

        };

        function init() {
            // move into the top of the screen to focus on the product detail area
            // utilService.scrollToTop();

            // load product detailed info when product is provided
            if (productId !== 0) {                
                // get product by id
                getProduct(productId, token);
                // check if product is favorid when user is logged in
                if (token && token !== '') {
                    isFavoritedProduct(token, productId);
                }
            }
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
        function isFavoritedProduct(userToken, productId) {
            userFavoriteDataFactory.isFavoriteProduct(userToken, productId)
                .success(function (status) {
                    if (status) {
                        vm.isFavorited = true;
                    }
                })
                .error(function (error) {
                    // ignore the 401 error
                });
        }

        // add favorite
        function addFavorite(token, favorite) {
            userFavoriteDataFactory.add(token, favorite)
                .then(function successCallback(response) {
                    vm.isFavorited = true;
                    
                }, function errorCallback(response) {                    
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
            userFavoriteDataFactory.remove(token, productId)
                .then(function successCallback(response) {
                    vm.isFavorited = false;

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