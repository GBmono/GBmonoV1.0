handpickSearch.factory('websiteKeywordFactory', function ($http, $rootScope, $q) {
    var factory = {};
    factory.getAllWebsites = function (url) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/GetWebsiteKeyword',
            data: { 'url': url },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.deleteWebsiteKeyword = function (id) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/DeleteWebsiteKeyword',
            data: { 'id': id },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.addWebsiteKeyword = function (websiteId, keywordName, xPathScript, isMultiple, isAttribute, attributeName, isSplitString, splitKey, needDownloadFile) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/AddWebsiteKeyword',
            data: { 'websiteId': websiteId, 'keywordName': keywordName, 'xPathScript': xPathScript, 'isMultiple': isMultiple, 'isAttribute': isAttribute, 'attributeName': attributeName, 'isSplitString': isSplitString, 'splitKey': splitKey, 'needDownloadFile': needDownloadFile },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.previewData = function (websiteId) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/PreviewExtractKeyword',
            data: { 'websiteId': websiteId },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.getLiveRecipesUrl = function (websiteId) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/GetTenLiveRecipes',
            data: { 'websiteId': websiteId },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.extractDataToDb = function (websiteId, keywordTypes) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/ExecuteGetDataToDb',
            data: { 'websiteId': websiteId, 'keywrodTypes': keywordTypes },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }

    factory.getRealTimeState = function (websiteId) {
        var deferred = $q.defer();
        $http({
            method: 'Post',
            url: '/WebsiteKeyWord/GetRealTimeStates',
            data: { 'websiteId': websiteId },
            headers: { 'Content-Type': 'application/json' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (reason) {
            deferred.reject(reason);
        });
        return deferred.promise;
    }




    return factory;
});

handpickSearch.controller('websiteKeywordController', function ($scope, websiteKeywordFactory, $location) {
    $scope.selected = {};

    var url = $location.search().websiteUrl;
    websiteKeywordFactory.getAllWebsites(url).then(
            function (data) {
                $scope.websiteKeywordTypes = data;
            });

    $scope.deleteWebsiteKeyword = function (index, id) {
        websiteKeywordFactory.deleteWebsiteKeyword(id).then(
            function (data) {
                $scope.websiteKeywordTypes.WebsiteKeywords.splice(index, 1);
            }
        );
    }

    $scope.addWebsiteKeyword = function () {
        var websiteId = $scope.websiteKeywordTypes.WebSiteId;
        var keywordName = $scope.keywordName;
        var xpath = $scope.xPathScript;
        var isMultiple = $scope.isMultiple;
        var isAttribute = $scope.isAttribute;
        var attributeName = $scope.AttributeName;
        var isSplitString = $scope.isSplitString;
        var splitKey = $scope.SplitKey;
        var needDownloadFile = $scope.needDownloadFile;

        websiteKeywordFactory.addWebsiteKeyword(websiteId, keywordName, xpath, isMultiple, isAttribute, attributeName, isSplitString, splitKey, needDownloadFile).then(
            function (data) {
                $scope.websiteKeywordTypes.WebsiteKeywords.push(data);

                $scope.keywordName = null;
                $scope.xPathScript = null;
                $scope.isMultiple = false;
                $scope.isAttribute = false;
                $scope.AttributeName = null;

            }
        );
    }

    $scope.previewData = function () {
        $scope.previewDataResult = null;
        var websiteId = $scope.websiteKeywordTypes.WebSiteId;
        websiteKeywordFactory.previewData(websiteId).then(
            function (data) {
                $scope.previewDataResult = data;
            }
        );
    }

    $scope.getRealTimeState = function () {
        $scope.realTimeStates = null;
        var websiteId = $scope.websiteKeywordTypes.WebSiteId;

        websiteKeywordFactory.getRealTimeState(websiteId).then(
            function (data) {
                $scope.realTimeStates = data;
            }
        );
    }


    $scope.extractDatatoDb = function () {
        var selectedKeywordType = $.grep($scope.websiteKeywordTypes.WebsiteKeywords, function (record) {
            return $scope.selected[record.KeyWordTypeId];
        });

        if (selectedKeywordType.length != 0) {
            var websiteId = $scope.websiteKeywordTypes.WebSiteId;
            websiteKeywordFactory.extractDataToDb(websiteId, selectedKeywordType).then(
               function (data) {
                   alert("Finished");
               }
           );
        }
    }


    $scope.getLiveRecipesUrl = function () {
        var websiteId = $scope.websiteKeywordTypes.WebSiteId;
        websiteKeywordFactory.getLiveRecipesUrl(websiteId).then(
              function (data) {
                  $scope.liveRecipesUrl = data;
              }
          );
    }

});