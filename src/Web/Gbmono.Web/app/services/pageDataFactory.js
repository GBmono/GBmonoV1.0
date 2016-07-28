/*
    retreive page data (menu, footer data) from json file
*/
(function (module) {
    factory.$inject = ['$http'];

    module.factory('pageDataFactory', factory);

    function factory($http) {
        // json data directory
        var pageDataPath = '/gbmono/app/json/';

        // return factory class
        return {
            getSilders: getSilders
        }

        function getSilders() {
            return $http.get(pageDataPath + '/slider.json');
        }

    }
})(angular.module('gbmono'));
