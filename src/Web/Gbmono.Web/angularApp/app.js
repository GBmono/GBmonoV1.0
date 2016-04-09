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
    angular.module('gbmono', ['ngRoute', 'ngAnimate', 'LocalStorageModule']).config(config);

    // module config
    // config route & http
    function config($routeProvider, $httpProvider) {
        // configure routes
        $routeProvider
                .when('/', { // home page
                    templateUrl: gbmono.app_view_path + '/home/home.html',
                    controller: 'homeController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/categories', { // all categories page
                    templateUrl: gbmono.app_view_path + '/categories/list.html',
                    controller: 'categoryListController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/products/:id', { // product detail page
                    templateUrl: gbmono.app_view_path + '/products/detail.html',
                    controller: 'productDetailController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/categories/:id/products/', { // product list
                    templateUrl: gbmono.app_view_path + '/products/list.html',
                    controller: 'productListController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/pin', { // products pin
                    templateUrl: gbmono.app_view_path + '/products/pin.html',
                    controller: 'productPinController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/topics', { // topics
                    templateUrl: gbmono.app_view_path + '/products/topic.html',
                    controller: 'topicController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/ranking', { // ranking
                    templateUrl: gbmono.app_view_path + '/products/ranking.html',
                    controller: 'productRankingController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/brands', { // brand list page
                    templateUrl: gbmono.app_view_path + '/brands/list.html',
                    controller: 'brandListController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/retailshops', { // retail shops browse 
                    templateUrl: gbmono.app_view_path + '/retailers/shoplist.html',
                    controller: 'retailShopBrowseController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/retailshops/search', { // retail shops search
                    templateUrl: gbmono.app_view_path + '/retailers/shops.html',
                    controller: 'retailShopSearchController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/articles', { // articles, news, information
                    templateUrl: gbmono.app_view_path + '/articles/index.html',
                    controller: 'articleIndexController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/login', { // login $ registration
                    templateUrl: gbmono.app_view_path + '/accounts/login.html',
                    controller: 'loginController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/profile', { // user profile
                    templateUrl: gbmono.app_view_path + '/accounts/profile.html',
                    controller: 'profileController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })               
                .otherwise({
                    templateUrl: gbmono.app_view_path + '/home/home.html',
                    controller: 'homeController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                });
    }
})();

