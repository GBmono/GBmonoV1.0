// factory class that handles CURD calls to the REST service
// factories are singletons by default so the object returned by the factory can be re-used over and over by different controllers in the application. 
// While AngularJS services can also be used to perform this type of functionality, a service returns an instance of itself (it’s also a singleton) and uses the “this” keyword as a result. 
// Factories on the other hand are free to create their own objects inside of the factory function and return them. 

/* sample data factory */
// create root level app module
// it seems it changes since the version 1.2.3
// it has to reference the angular-route.js file and define the controllers parameter
// define angularJs module
(function () {
    // inject the parameters
    config.$inject = ['$routeProvider', '$httpProvider'];


    // config
    // ng-route, ng-animate, kendo-ui modules
    angular.module('gbmono', ['ngRoute', 'ngAnimate', 'LocalStorageModule', 'kendo.directives']).config(config);

    // module config
    // config route & http
    function config($routeProvider, $httpProvider) {
        // inject authentication interceptor
        // bearer token authentication
        // $httpProvider
        $httpProvider.interceptors.push('authInterceptor');

        // configure routes
        $routeProvider
                .when('/dashboard', { // home page
                    templateUrl: gbmono.app_view_path + '/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    caseInsensitiveMatch: true
                })
                .when('/products', { // 默认商品搜索页
                    templateUrl: gbmono.app_view_path + '/products/search.html',
                    controller: 'productSearchController',
                    caseInsensitiveMatch: true
                })
                .when('/categories/:id/products', { // 分类商品页
                    templateUrl: gbmono.app_view_path + '/products/browse.html',
                    controller: 'productBrowseController',
                    caseInsensitiveMatch: true
                })
                .when('/products/create', { // 商品创建页
                    templateUrl: gbmono.app_view_path + '/products/createProduct.html',
                    controller: 'productCreateController',
                    caseInsensitiveMatch: true
                })
                .when('/products/create/:id/extra', { // 商品图片创建页
                    templateUrl: gbmono.app_view_path + '/products/createProductExtra.html',
                    controller: 'productExtraInfoCreateController',
                    caseInsensitiveMatch: true
                })
                .when('/products/edit/:id', { // 商品编辑页
                    templateUrl: gbmono.app_view_path + '/products/edit.html',
                    controller: 'productEditController',
                    caseInsensitiveMatch: true
                })
                .when('/tags', { // 标签
                    templateUrl: gbmono.app_view_path + '/tags/list.html',
                    controller: 'tagListController',
                    caseInsensitiveMatch: true
                })
                .when('/brands', { // 品牌(商)
                    templateUrl: gbmono.app_view_path + '/brands/list.html',
                    controller: 'brandListController',
                    caseInsensitiveMatch: true
                })
                .when('/categories', { // top category
                    templateUrl: gbmono.app_view_path + '/categories/top.html',
                    controller: 'topCategoryController',
                    caseInsensitiveMatch: true
                })
                .when('/categories/:parentId/second', { // second category
                    templateUrl: gbmono.app_view_path + '/categories/second.html',
                    controller: 'secondCategoryController',
                    caseInsensitiveMatch: true
                })
                .when('/categories/:parentId/third', { // third category
                    templateUrl: gbmono.app_view_path + '/categories/third.html',
                    controller: 'thirdCategoryController',
                    caseInsensitiveMatch: true
                })
                .when('/retailers', { // retailer list page
                    templateUrl: gbmono.app_view_path + '/retailers/list.html',
                    controller: 'retailerListController',
                    caseInsensitiveMatch: true
                })
                .when('/retailershops', { // retailer shop list page
                    templateUrl: gbmono.app_view_path + '/retailers/shop.html',
                    controller: 'retailerShopListController',
                    caseInsensitiveMatch: true
                })
                .otherwise({
                    templateUrl: gbmono.app_view_path + '/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    caseInsensitiveMatch: true
                });
    }


})();

