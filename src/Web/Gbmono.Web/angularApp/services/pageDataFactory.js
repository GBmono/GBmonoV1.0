﻿/*
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
            getSilders: getSilders,
            getArticles:getArticles,
            getNews: getNews,
            getFooterData: getFooterData
        }

        function getSilders() {
            return $http.get(pageDataPath + '/slider.json');
        }

        function getArticles() {
            return $http.get(pageDataPath + '/article.json');
        }

        function getNews() {
            return $http.get(pageDataPath + '/news.json');
        }

        function getFooterData() {
            return $http.get(pageDataPath + '/footer.json');
        }
    }
})(angular.module('gbmono'));
