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
                .otherwise({
                    templateUrl: gbmono.app_view_path + '/dashboard/dashboard.html',
                    controller: 'dashboardController',
                    caseInsensitiveMatch: true
                });
    }


})();

