/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    '$timeout',
                    'utilService',
                    'pluginService',
                    'userFavoriteDataFactory'];

    // create controller
    module.controller('profileController', ctrl);

    // controller body
    function ctrl($location,
                  $timeout,
                  utilService,
                  pluginService,
                  userFavoriteDataFactory) {
        // controller 
        var vm = this;
        // user saved products
        vm.products = [];

        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };

        // indicate if all data is loaded
        vm.isAllDataLoaded = false;
        // indicate if data requesting starts
        vm.isLoadingStarts = false;

        // get token from local storage
        var token = utilService.getToken();
        
        init(); // page init 

        function init() {            
            // if no token
            if (!token || token === '') {
                // redirect into login page
                $location.path('/login');
            }
           
            // get user saved products
            getSavedProducts(token, vm.paging.pageIndex, vm.paging.pageSize);

            // attach scroll event handler
            angular.element(window).unbind('scroll').scroll(function () {
                var top = angular.element(window).scrollTop();
                var bottom = angular.element(document).height() - angular.element(window).height() - 50;
                // when it still has more data to be loaded
                if (top > bottom && !vm.isAllDataLoaded) {
                    // avoild sending multiple requests
                    if (!vm.isLoadingStarts) {
                        // load more products when scrolling at the bottom
                        getSavedProducts(token,
                                    vm.paging.pageIndex, // first page
                                    vm.paging.pageSize); // page size
                    }
                }
            });
        }

        // get saved products
        function getSavedProducts(userToken, pageIndex, pageSize) {
            // data requesting starts
            vm.isLoadingStarts = true;

            // call service
            userFavoriteDataFactory.getSavedProducts(userToken, pageIndex, pageSize)
                .then(function successCallback(response) {
                    // add products into collection
                    vm.products = vm.products.concat(response.data);
                    // add page index
                    vm.paging.pageIndex++;
                    // if no more products 
                    if (response.data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                        console.log('finished');
                    }
                   
                    // data requesting starts
                    vm.isLoadingStarts = false;

                }, function errorCallback(response) {
                    console.log(response);
                    // if user is not authorized
                    if (response.status === 401) {
                        // direct into login page
                        // todo: returnUrl
                        $location.path('/login');
                    }
                });
        }
    }
})(angular.module('gbmono'));

/*
    user saved articles
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    '$timeout',
                    'utilService',
                    'pluginService',
                    'userFavoriteDataFactory'];

    // create controller
    module.controller('profileArticleController', ctrl);

    // controller body
    function ctrl($location,
                  $timeout,
                  utilService,
                  pluginService,
                  userFavoriteDataFactory) {
        // controller 
        var vm = this;
        // user saved products
        vm.articles = [];

        // user name
        vm.name = '';
        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };

        // indicate if all data is loaded
        vm.isAllDataLoaded = false;
        // indicate if data requesting starts
        vm.isLoadingStarts = false;

        // get token from local storage
        var token = utilService.getToken();

        // view event handler
        vm.logout = function () {
            // clear token
            utilService.clearToken();
            // redirect into login page or home page??
            $location.path('/login');
        };

        init(); // page init 

        function init() {
            // if no token
            if (!token || token === '') {
                // redirect into login page
                $location.path('/login');
            }

            // get current user name
            vm.name = utilService.getUserName();

            // get user saved products
            getSavedArticles(token, vm.paging.pageIndex, vm.paging.pageSize);

            // attach scroll event handler
            angular.element(window).unbind('scroll').scroll(function () {
                var top = angular.element(window).scrollTop();
                var bottom = angular.element(document).height() - angular.element(window).height() - 50;
                // when it still has more data to be loaded
                if (top > bottom && !vm.isAllDataLoaded) {
                    // avoild sending multiple requests
                    if (!vm.isLoadingStarts) {
                        // load more products when scrolling at the bottom
                        getSavedArticles(token,
                                    vm.paging.pageIndex, // first page
                                    vm.paging.pageSize); // page size
                    }
                }
            });
        }

        // get saved articles
        function getSavedArticles(userToken, pageIndex, pageSize) {
            // data requesting starts
            vm.isLoadingStarts = true;

            // call service
            userFavoriteDataFactory.getSavedArticles(userToken, pageIndex, pageSize)
                .then(function successCallback(response) {
                    // add products into collection
                    vm.articles = vm.articles.concat(response.data);
                    // add page index
                    vm.paging.pageIndex++;
                    // if no more products 
                    if (response.data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                    }

                    vm.isLoadingStarts = false;

                }, function errorCallback(response) {
                    // if user is not authorized
                    if (response.status === 401) {
                        // direct into login page
                        // todo: returnUrl
                        $location.path('/login');
                    }
                });
        }

    }
})(angular.module('gbmono'));