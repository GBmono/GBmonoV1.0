/*
    authentication intercept service
*/

(function(module) {
    // inject the params
    intercepter.$inject = ['$q'];

    // create instance
    module.factory('authInterceptor', intercepter);

    function intercepter($q) {
        // return intercepter
        return {
            request: beforeRequestSend, // attach bearer token before each http request
            responseError: error // process authorization error
        };

        // attach bearer token http header into the request before sending
        function beforeRequestSend(config) {
            config.headers = config.headers || {};
            // retreive token value from local storage
            if (window.localStorage.getItem(gbmono.LOCAL_STORAGE_TOKEN_KEY) &&
                window.localStorage.getItem(gbmono.LOCAL_STORAGE_TOKEN_KEY) !== '') {
                // set the token value into the authorization header
                config.headers.Authorization = 'Bearer ' +
                                                window.localStorage.getItem(gbmono.LOCAL_STORAGE_TOKEN_KEY);
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
                    // window.location = metsys.APP_NAME + '/login.html';
                } else {
                    // extract the route if exists
                    var index = url.indexOf('#') + 2;
                    // failed to extract the route
                    if (index >= url.length) {
                        // login page
                        // window.location = metsys.APP_NAME + '/login.html';
                    } else {
                        var returnUrl = url.substring(index);
                        // login page with returl url
                        // window.location = metsys.APP_NAME + '/login.html?returnUrl=' + returnUrl;
                    }
                }                
            }
            else if (rejection.status === '403') {
                // user is authenticated but don't have permission to access the resource
                // return to unauthorized page
                // window.location = metsys.APP_NAME + '/401.html';
            }
            // return error object
            return $q.reject(rejection);
        }

    }
})(angular.module('metsys'));