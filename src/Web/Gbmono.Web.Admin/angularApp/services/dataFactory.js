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
            login: login
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
            getById:getById,
            getTopCategories: getTopCategories,
            getByParent: getByParent,
            getTreeviewItems: getTreeviewItems,
            create: create,
            update: update
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

        function create(category) {
            return $http.post(gbmono.api_site_prefix.category_api_url, category);
        }

        function update(category) {
            return $http.put(gbmono.api_site_prefix.category_api_url + '/' + category.categoryId, category);
        }
    }

})(angular.module('gbmono'));

/*
    tag data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('tagDataFactory', factory);

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
            return $http.get(gbmono.api_site_prefix.tag_api_url);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.tag_api_url + '/' + id);
        }

        function create(tag) {
            return $http.post(gbmono.api_site_prefix.tag_api_url, tag);
        }

        function update(tag) {
            return $http.put(gbmono.api_site_prefix.tag_api_url + '/' + tag.tagId, tag);
        }

        function del(id) {
            return $http.delete(gbmono.api_site_prefix.tag_api_url + '/' + id);
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
            getCountByCategory: getCountByCategory,
            search: search,
            getTags: getTags,
            saveTags: saveTags,
            create: create,
            update: update
        };

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.product_api_url + "/" + id);
        }

        function getByCategory(categoryId) {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/Categories/' + categoryId);
        }

        function search(model) {
            return $http.post(gbmono.api_site_prefix.product_api_url + '/Search', model);
        }
        
        function getTags(id) {
            return $http.get(gbmono.api_site_prefix.product_api_url + "/" + id + '/Tags');
        }

        function saveTags(model) {
            return $http.post(gbmono.api_site_prefix.product_api_url + '/SaveTags', model);
        }

        function getCountByCategory() {
            return $http.get(gbmono.api_site_prefix.product_api_url + '/CountByTopCategory');
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
            getByProduct: getByProduct,
            update: update,
            remove: remove
        };

        function getByProduct(productId) {
            return $http.get(gbmono.api_site_prefix.product_image_api_url + "/Products/" + productId);
        }

        function update(image) {
            return $http.put(gbmono.api_site_prefix.product_image_api_url + '/' + image.productImageId, image);
        }

        function remove(id) {
            return $http.delete(gbmono.api_site_prefix.product_image_api_url + '/' + id);
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
    stats data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('statsDataFactory', factory);

    // factory implement
    function factory($http) {

        // return data factory with CRUD calls
        return {
            getSiteStats: getSiteStats
        };

        function getSiteStats() {
            return $http.get(gbmono.api_site_prefix.stats_api_url + '/Site');
        }

    }

})(angular.module('gbmono'));


/*
    retailer shops data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('retailerShopsDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getByShopsByRetailerId: getByShopsByRetailerId,
            getById:getById,
            update: update
        }

        function getByShopsByRetailerId(id) {
            return $http.get(gbmono.api_site_prefix.retailer_shop_api_url + "/Retailer/" + id);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.retailer_shop_api_url + "/" + id);
        }

        function update(retailerShop) {
            return $http.put(gbmono.api_site_prefix.retailer_shop_api_url + '/' + retailerShop.retailerShopId, retailerShop);
        }
    }

})(angular.module('gbmono'));



/*
    country data factory
*/
//(function (module) {
//    // inject params
//    factory.$inject = ['$http'];

//    // create instance
//    module.factory('countryDataFactory', factory);

//    // factory implement
//    function factory($http) {

//        // return data factory with CRUD calls
//        return {
//            getAll: getAll,
//            getById: getById,
//            create: create,
//            update: update,
//            del: del
//        };

//        function getAll() {
//            return $http.get(gbmono.api_site_prefix.country_api_url);
//        }

//        function getById(id) {
//            return $http.get(gbmono.api_site_prefix.country_api_url + '/' + id);
//        }

//        function create(country) {
//            return $http.post(gbmono.api_site_prefix.country_api_url, country);
//        }

//        function update(country) {
//            return $http.put(gbmono.api_site_prefix.country_api_url + '/' + country.countryId, country);
//        }

//        function del(id) {
//            return $http.delete(gbmono.api_site_prefix.country_api_url + '/' + id);
//        }
//    }

//})(angular.module('gbmono'));
