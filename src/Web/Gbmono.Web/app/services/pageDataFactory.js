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
            getSilders: getSilders,
            getArticles:getArticles,
            getNews: getNews,
            getTopicItems:getTopicItems,
            getFooterData: getFooterData
        }

        function getSilders() {
            return $http.get(pageDataPath + '/slider.json');
        }

        function getArticles() {
            return $http.get(pageDataPath + '/article.json?v=1.2');
        }

        function getNews() {
            return $http.get(pageDataPath + '/news.json');
        }

        function getFooterData() {
            return $http.get(pageDataPath + '/footer.json');
        }

        // return topic data
        function getTopicItems() {
            return $http.get(pageDataPath + '/topic_premium.json');
        }
    }
})(angular.module('gbmono'));
