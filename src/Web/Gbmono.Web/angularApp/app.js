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
                .when('/products/:id', { // 商品详细页
                    templateUrl: gbmono.app_view_path + '/products/detail.html',
                    controller: 'productDetailController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/categories/:id/products/', { // 商品列表页
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
                .when('/topics', { // 推荐， 专题
                    templateUrl: gbmono.app_view_path + '/products/topic.html',
                    controller: 'topicController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/brands', { // 品牌商列表页
                    templateUrl: gbmono.app_view_path + '/brands/list.html',
                    controller: 'brandListController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/articles', { // 信息，文章，新闻
                    templateUrl: gbmono.app_view_path + '/articles/index.html',
                    controller: 'articleIndexController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/login', { // 登陆注册页面
                    templateUrl: gbmono.app_view_path + '/accounts/login.html',
                    controller: 'loginController',
                    controllerAs: 'vm',
                    caseInsensitiveMatch: true
                })
                .when('/profile', { // 用户中心
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

