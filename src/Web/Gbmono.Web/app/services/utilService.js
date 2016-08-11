/*
    utility service, common functions
*/
(function (module) {
    svr.$inject = ['localStorageService'];

    module.service('utilService', svr);

    function svr(localStorageService) {
        /* gbmono bearer token key name */
        BEARER_TOKEN_KEY = 'gbmono_BEARER_TOKEN';
        /* gbmobno user name key name */
        USERNAME_KEY = 'gbmono_USER_NAME';
        /* gbmono visited products kye name*/
        VISITED_PRODUCTS_KEY = 'gbmono_VISITED_PRODUCTS';

        return {
            getToken: getToken,
            saveToken: saveToken,
            getUserName: getUserName,
            saveUserName: saveUserName,
            clearToken: clearToken,
            scrollToTop: scrollToTop,
            redirectToLoginPage: redirectToLoginPage,
            redirectBack: redirectBack,
            saveVisitProduct: saveVisitProduct,
            getVisitedProducts: getVisitedProducts            
        };
        
        // return user token stored in local storage or cookie (if local storage is not supported)
        function getToken() {
            return localStorageService.get(BEARER_TOKEN_KEY);
        }

        function saveToken(token) {
            // save bearer token
            localStorageService.set(BEARER_TOKEN_KEY, token);
        }

        function getUserName() {
            return localStorageService.get(USERNAME_KEY);
        }

        function saveUserName(name) {
            localStorageService.set(USERNAME_KEY, name);
        }
        
        function clearToken() {
            // clear token
            localStorageService.set(BEARER_TOKEN_KEY, '');
            // clear user name
            localStorageService.set(USERNAME_KEY, '');
        }

        // move to top
        function scrollToTop() {
            $('html, body').animate({ scrollTop: 0 }, 'fast');
        }

        // redirect to login page with return url
        function redirectToLoginPage(route, params) {
            var subUrl = encodeURIComponent(route + '/' + params);

            // login page with returl url
            window.location = '#/login?returnUrl=' + subUrl;
        }

        // redirect to return page
        function redirectBack() {
            // get the url, decode the url
            // convert into lower cases
            var url = decodeURIComponent(window.location.href).toLowerCase();

            // sample: #/login?returnUrl=products/1
            if (url.indexOf('returnurl=') != -1) {
                // get the index of the route name
                var index = url.indexOf('returnurl=') + 10;
                // get the index of the last slash
                var indexOfLastSlash = url.lastIndexOf("/");
                // extract the route name and route params
                var routeName = url.substring(index, indexOfLastSlash);
                var routeParams = url.substring(indexOfLastSlash + 1);
                
                // redirect
                window.location = '#/' + routeName + '/' + routeParams;
            }
            else {
                window.location = '#/profile'; // profile page
            }
        }

        // save visited product
        function saveVisitProduct(product) {
            var visitedProducts = localStorageService.get(VISITED_PRODUCTS_KEY);
            if (!visitedProducts) {
                visitedProducts = [];
            }

            // maxium visited products is 2
            // add product into list
            if (visitedProducts.length < 2) {
                visitedProducts.push(product);
            }
            else {
                // remove the first (earliest) product
                visitedProducts.shift();
                // add product
                visitedProducts.push(product);
            }

            // save into local storage
            localStorageService.set(VISITED_PRODUCTS_KEY, visitedProducts);
        }

        // return visited products
        function getVisitedProducts() {
            var visitedProducts = localStorageService.get(VISITED_PRODUCTS_KEY);
            if (!visitedProducts) {
                visitedProducts = [];
            }

            return visitedProducts;
        }
    }
   
})(angular.module('gbmono'));