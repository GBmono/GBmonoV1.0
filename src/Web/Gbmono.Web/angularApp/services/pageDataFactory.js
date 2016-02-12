/*
    retreive page data (menu, footer data) from json file
*/
(function (module) {
    factory.$inject = ['$http'];

    module.factory('pageDataFactory', factory);

    function factory($http) {
        // json data directory
        var pageDataPath = '/angularApp/data/';

        // return factory class
        return {
            getMenuData: getMenuData,
            getFooterData: getFooterData
        }

        function getMenuData() {
            return $http.get(pageDataPath + '/menu.json');
        }

        function getFooterData() {
            return $http.get(pageDataPath + '/footer.json');
        }
    }
})(angular.module('gbmono'));
