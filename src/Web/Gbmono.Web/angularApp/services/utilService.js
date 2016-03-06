/*
    utility service, common functions
*/
(function (module) {
    svr.$inject = ['localStorageService'];

    module.service('utilService', svr);

    function svr(localStorageService) {
        /* gbmono bearer token key name */
        BEARER_TOKEN_KEY = 'gbmono_BEARER_TOKEN'; // localstorage token key name
        USERNAME_KEY = 'gbmono_USER_NAME';

        return {
            getToken: getToken,
            saveToken: saveToken,
            getUserName: getUserName,
            saveUserName: saveUserName,
            scrollToTop: scrollToTop
        };
        
        // return user token stored in local storage or cookie (if local storage is not supported)
        function getToken() {
            return localStorageService.get(BEARER_TOKEN_KEY);
        }

        function saveToken(token) {
            // save bearer token
            localStorageService.set(BEARER_TOKEN_KEY, token);
        }

        function getUserName() {
            return localStorageService.get(USERNAME_KEY);
        }

        function saveUserName(name) {
            localStorageService.set(USERNAME_KEY, name);
        }

        // move to top
        function scrollToTop() {
            $('html, body').animate({ scrollTop: 0 }, 'fast');
        }
    }

    
})(angular.module('gbmono'));