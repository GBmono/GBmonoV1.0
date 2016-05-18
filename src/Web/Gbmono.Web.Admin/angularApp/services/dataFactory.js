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
            getById: getById,
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
            getByType: getByType,
            getById: getById,
            create: create,
            update: update,
            del: del
        };

        function getAll() {
            return $http.get(gbmono.api_site_prefix.tag_api_url);
        }

        function getByType(typeId) {
            return $http.get(gbmono.api_site_prefix.tag_api_url + '/Types/' + typeId);
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
    productTag data factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('productTagDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getByProductId: getByProductId
        };

        function getByProductId(id) {
            return $http.get(gbmono.api_site_prefix.product_tag_api_url + "/Product/" + id);
        }
    
    }

})(angular.module('gbmono'));

/*
    brand collection factory
*/
(function (module) {
    // inject params
    factory.$inject = ['$http'];

    // create instance
    module.factory('brandCollectionDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getByBrandId: getByBrandId
        };

        function getByBrandId(id) {
            return $http.get(gbmono.api_site_prefix.brand_collection_api_url + "/Brand/" + id);
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
    module.factory('retailerShopDataFactory', factory);

    // factory implement
    function factory($http) {
        // return data factory with CRUD calls
        return {
            getbyRetailerCity: getbyRetailerCity,
            getById: getById,
            update: update
        }

        function getbyRetailerCity(retailerId, cityId) {
            return $http.get(gbmono.api_site_prefix.retailer_shop_api_url + '/Retailer/' + retailerId + '/City/' + cityId);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.retailer_shop_api_url + "/" + id);
        }

        function update(shop) {
            return $http.put(gbmono.api_site_prefix.retailer_shop_api_url + '/' + shop.retailShopId, shop);
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
            getByDate: getByDate,
            getById: getById,
            create: create,
            update: update,
            del: del
        };

        function getByDate(from, to, type) {
            return $http.get(gbmono.api_site_prefix.article_api_url + '/' + from + '/' + to + '/' + type);
        }

        function getById(id) {
            return $http.get(gbmono.api_site_prefix.article_api_url + '/' + id);
        }

        function create(article) {
            return $http.post(gbmono.api_site_prefix.article_api_url, article);
        }

        function update(article) {
            return $http.put(gbmono.api_site_prefix.article_api_url + '/' + article.articleId, article);
        }

        function del(id) {
            return $http.delete(gbmono.api_site_prefix.article_api_url + '/' + id);
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
