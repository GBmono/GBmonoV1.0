/*
    authentication intercept service
*/

(function(module) {
    // inject the params
    intercepter.$inject = ['$q', 'utilService'];

    // create instance
    module.factory('authInterceptor', intercepter);

    function intercepter($q, utilService) {
        // return intercepter
        return {
            request: beforeRequestSend, // attach bearer token before each http request
            responseError: error // process authorization error
        };

        // attach bearer token http header into the request before sending
        function beforeRequestSend(config) {
            config.headers = config.headers || {};
            // retreive token value from local storage
            var token = utilService.getToken();

            // check if token exists
            if (token && token !== '') {
                // set the token value into the authorization header
                config.headers.Authorization = 'Bearer ' + token;
            }
            // return config object
            return config;
        }

        // process auth error from response
        function error(rejection) {
            // if Unauthorized error return            
            if (rejection.status === 401) {
                // redirect to login page when user is not authorized 
                // retreive the return url (route)               
                var url = window.location.href;
                // if current url contains any angularJs route
                if (url.indexOf('#') === -1) {
                    // login page
                    window.location = gbmono.html_app + '/login.html';
                } else {
                    // extract the route if exists
                    var index = url.indexOf('#') + 2;
                    // failed to extract the route
                    if (index >= url.length) {
                        // login page
                        window.location = gbmono.html_app + '/login.html';
                    } else {
                        var returnUrl = url.substring(index);
                        // login page with returl url
                        window.location = gbmono.html_app + '/login.html?returnUrl=' + returnUrl;
                    }
                }                
            }
            else if (rejection.status === '403') {
                // user is authenticated but don't have permission to access the resource
                // return to unauthorized page

            }
            // return error object
            return $q.reject(rejection);
        }

    }
})(angular.module('gbmono'));