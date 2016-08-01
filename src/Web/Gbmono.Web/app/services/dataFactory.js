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
            login: login
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
            getById:getById,
            getTopCates: getTopCates,
            getBrands: getBrands
        }

        // update version number when json file is updated
        // so data factory would download new json file from server instead of loading cached files
        // var version = '1.0';

        // read cached data from json file
        function getAll() {
            // return $http.get('/gbmono/angularApp/data/category.json?v=' + version);
            return $http.get(gbmono.api_site_prefix.category_api_url);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/' + id);
        }

        function getTopCates() {
            // return $http.get('/gbmono/angularApp/data/category_top.json?v=' + version);
            return $http.get(gbmono.api_site_prefix.category_api_url + '/Top');
        }
        
        function getBrands(id, levelId) {
            return $http.get(gbmono.api_site_prefix.category_api_url + '/' + id + '/Brands/' + levelId);
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
            getByRanking: getByRanking,
            getByCategory: getByCategory,
            getByBrand: getByBrand,
            getById: getById
        };

        function getNewProducts(pageIndex, pageSize) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/New/' + pageIndex + '/' + pageSize);
        }

        function getByCategory(categoryId, pageIndex, pageSize) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Categories/' + categoryId + '/' + pageIndex + '/' + pageSize);
        }
        
        function getByBrand(brandId, pageIndex, pageSize) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Brands/' + brandId + '/' + pageIndex + '/' + pageSize);
        }

        function getByRanking() {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Ranking');
        }

        function getById(id, token) {
            // attach token if it exits so api could capture user action
            if (token && token != '') {
                attachToken(token);
            }

            return $http.get(gbmono.api_site_prefix.product_api_url + '/' + id);
        }

        // private method
        function attachToken(token) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + token;
        }

    }

})(angular.module('gbmono'));

/*
 retailer data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('retailerDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getAll: getAll
        };

        function getAll() {
            return $http.get(gbmono.api_site_prefix.retailer_api_url);
        }

    }

})(angular.module('gbmono'));

/*
 retailer shop data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('retailerShopDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getByCity: getByCity,
            search: search
        };

        function getByCity(retailerId, cityId) {
            return $http.get(gbmono.api_site_prefix.retailer_shop_api_url + '/Retailer/' + retailerId +  '/City/' + cityId);
        }

        function search(model) {
            return $http.post(gbmono.api_site_prefix.retailer_shop_api_url + '/Search', model);
        }

    }

})(angular.module('gbmono'));

/*
    user favorite data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('userFavoriteDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getFavoriteProducts: getFavoriteProducts,
            isFavoriteProduct: isFavoriteProduct,
            add: add,
            remove:remove
        };

        function getFavoriteProducts(token, pageIndex, pageSize) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token);
            return $http.get(gbmono.api_site_prefix.userfavorite_api_url + '/Products/' + pageIndex + '/' + pageSize);
        }

        function isFavoriteProduct(token, productId) {
            attachToken(token);
            return $http.get(gbmono.api_site_prefix.userfavorite_api_url + '/IsFavorited/' + productId);
        }

        function add(token, favorite) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token); 
            return $http.post(gbmono.api_site_prefix.userfavorite_api_url, favorite);
        }

        function remove(token, id) {
            // it required authenticated token to access this method 
            // add token to authorization header
            attachToken(token);
            return $http.delete(gbmono.api_site_prefix.userfavorite_api_url + '/' + id);
        }

        // private method
        function attachToken(token) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + token;
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

        // get all brands
        // grouped by first alphabet
        function getAll() {
            return $http.get(gbmono.api_site_prefix.brand_api_url + '/GroupByAlphabet');
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.brand_api_url + '/' + id);
        }

    }

})(angular.module('gbmono'));

/*
    article data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('articleDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getById: getById,
            getByType: getByType,
            getByRanking: getByRanking
        };

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.article_api_url + '/' + id);
        };

        function getByType(typeId, pageIndex, pageSize) {            
            return $http.get(gbmono.api_site_prefix.article_api_url + '/List/' + typeId + '/' + pageIndex + '/' + pageSize);
        }

        function getByRanking() {
            return $http.get(gbmono.api_site_prefix.article_api_url + '/Ranking');
        }
    }

})(angular.module('gbmono'));

/*
    location data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('locationDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getStates: getStates,
            getCities: getCities
        };

        function getStates(countryId) {
            return $http.get(gbmono.api_site_prefix.location_api_url + '/' + countryId + '/States');
        }

        function getCities(stateId) {
            return $http.get(gbmono.api_site_prefix.location_api_url + '/' + stateId + '/Cities');
        }

    }

})(angular.module('gbmono'));

