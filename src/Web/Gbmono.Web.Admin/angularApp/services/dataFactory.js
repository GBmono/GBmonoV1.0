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
            getById:getById,
            getTopCategories: getTopCategories,
            getByParent: getByParent,
            getTreeviewItems: getTreeviewItems
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.category_api_url + "/" + id);
        }

        function getTopCategories() {
            return $http.get(gbmono.api_site_prefix.category_api_url + "/Top");
        }

        function getByParent(parentId) {
            return $http.get(gbmono.api_site_prefix.category_api_url + "/Parent/" + parentId);
        }

        function getTreeviewItems(parentId) {
            return $http.get(gbmono.api_site_prefix.category_api_url + "/Treeview/" + parentId);
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
            getById: getById,
            getByCategory: getByCategory,
            create: create,
            update: update
        };

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.product_api_url + "/" + id);
        }

        function getByCategory(categoryId) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Categories/' + categoryId);
        }

        function create(product) {
            return $http.post(gbmono.api_site_prefix.product_api_url, product);
        }

        function update(product) {
            return $http.put(gbmono.api_site_prefix.product_api_url + "/" + product.productId, product)
        }
    }

})(angular.module('gbmono'));


/*
    product image factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('productImageDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getByProduct:getByProduct
        };

        function getByProduct(productId) {
            return $http.get(gbmono.api_site_prefix.product_image_api_url + "/Products/" + productId);
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
            getById: getById,
            create: create,
            update: update,
            del: del
        };

        function getAll() {
            return $http.get(gbmono.api_site_prefix.brand_api_url);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.brand_api_url + '/' + id);
        }

        function create(brand) {
            return $http.post(gbmono.api_site_prefix.brand_api_url, brand);
        }

        function update(brand) {
            return $http.put(gbmono.api_site_prefix.brand_api_url + '/' + brand.brandId, brand);
        }

        function del(id) {
            return $http.delete(gbmono.api_site_prefix.brand_api_url + '/' + id);
        }
    }

})(angular.module('gbmono'));


/*
    country data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('countryDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getAll: getAll,
            getById: getById,
            create: create,
            update: update,
            del: del
        };

        function getAll() {
            return $http.get(gbmono.api_site_prefix.country_api_url);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.country_api_url + '/' + id);
        }

        function create(country) {
            return $http.post(gbmono.api_site_prefix.country_api_url, country);
        }

        function update(country) {
            return $http.put(gbmono.api_site_prefix.country_api_url + '/' + country.countryId, country);
        }

        function del(id) {
            return $http.delete(gbmono.api_site_prefix.country_api_url + '/' + id);
        }
    }

})(angular.module('gbmono'));
