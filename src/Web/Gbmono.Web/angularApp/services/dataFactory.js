/*
 account data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('accountDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            register: register,
            login: login,
            getFavorites: getUserFavorites,
            addFavorite: addUserFavorite,
            removeFavorite: removeUserFavorite
        }

        // register user
        function register(model) {
            return $http.post(gbmono.api_site_prefix.account_api_url + '/Register', model);
        }

        // login, get access bearer token
        function login(userName, password) {
            // user name and password is posted as 'application/x-www-form-urlencoded'
            return $http({
                url: gbmono.api_token_url,
                method: 'POST',
                data: "userName=" + userName + "&password=" + password + "&grant_type=password",
                headers: {
                    'content-type': 'application/x-www-form-urlencoded'
                }
            });
        }

        function getUserFavorites(token) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token);
            return $http.get(gbmono.api_site_prefix.account_api_url + '/Favorites');
        }

        function addUserFavorite(token, favorite) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token); console.log(favorite);
            return $http.post(gbmono.api_site_prefix.account_api_url + '/AddFavorite', favorite);
        }

        function removeUserFavorite(token, favorite) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token);
            return $http.post(gbmono.api_site_prefix.account_api_url + '/RemoveFavorite', favorite);
        }
        
        // private method
        function attachToken(token) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + token;
        }

        //function isAuthenticated(token) {
        //    // call authorized web api to validate if current token is valid
        //    // add token to authorization header
        //    $http.defaults.headers.common.Authorization = 'Bearer ' + token;
        //    // call authorized web method
        //    return $http.get(gbmono.api_site_prefix.account_api_url + '/Current');
        //}
    }
})(angular.module('gbmono'));


/*
    category data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('categoryDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getAll: getAll,
            getMenu: getMenu,
            getTopCates: getTopCates,
            getThirdCates:getThirdCates,
            getBrands: getBrands
        }
        // todo?
        function getAll() {
            return $http.get(gbmono.api_site_prefix.category_api_url);
        }

        function getMenu (id) {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/Menu/' + id);
        }

        function getTopCates() {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/Top');
        }

        function getThirdCates(id) {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/Third/' + id);
        }

        function getBrands(id) {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/' + id + '/Brands');
        }
    }

})(angular.module('gbmono'));


/*
    product data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('productDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getNewProducts: getNewProducts,
            getById: getById,
            getByCategory: getByCategory
        };

        function getNewProducts(pageIndex, pageSize) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/New/' + pageIndex + '/' + pageSize);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/' + id);
        }

        function getByCategory(categoryId) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Categories/' + categoryId);
        }


    }

})(angular.module('gbmono'));

/*
    brand data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('brandDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getAll: getAll,
            getById: getById
        };

        function getAll() {
            return $http.get(gbmono.api_site_prefix.brand_api_url);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.brand_api_url + '/' + id);
        }

    }

})(angular.module('gbmono'));



