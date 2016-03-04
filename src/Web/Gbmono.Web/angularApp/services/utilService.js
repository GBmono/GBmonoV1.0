﻿/*
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
            saveToken: saveToken
        };
        
        // return user token stored in local storage or cookie (if local storage is not supported)
        function getToken() {
            return localStorageService.get(BEARER_TOKEN_KEY);
        }

        function saveToken(token) {
            localStorageService.set(BEARER_TOKEN_KEY, token);
        }
    }

    
})(angular.module('gbmono'));