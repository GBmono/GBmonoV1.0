/*
    article detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$sce',
                    'utilService',
                    'pluginService',
                    'articleDataFactory',
                    'userFavoriteDataFactory'];

    // create controller
    module.controller('articleDetailController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $sce,
                  utilService,
                  pluginService,
                  articleDataFactory,
                  userFavoriteDataFactory) {
        var vm = this;
        // article
        vm.article = {};
        // flag which indicates if article is favorited
        vm.isSaved = false;
        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;

        // get artcile id from url
        var articleId = $routeParams.id ? parseInt($routeParams.id) : 0;
        // get token from local storage        
        var token = utilService.getToken();
        // user favorite model
        var userFavorite = { saveItemType: 2, keyId: articleId }; // article: save item type id = 2

        init(); // page init 

        // event handlers
        // save article
        vm.saveArticle = function () {
            // user is not authenticated if toke doesn't exist
            if (!token || token == '') {
                // redirect into login page with return url
                utilService.redirectToLoginPage('articles', articleId);
            }
                    
            // save article
            saveArticle(token, userFavorite);
        };

        // remove article 
        vm.removeArticle = function () {
            // remove article
            removeArticle(token, userFavorite);
        };

        // page init
        function init() {
            // invalida article id
            if (articleId == 0) {
                // todo
            }
            
            // get article detail by id
            getArticleById(articleId);

            // check if it's saved in user favorites
            isSaved(token, userFavorite);
        }
        
        // get article detail by id
        function getArticleById(id) {
            // loading progress
            pluginService.showDataLoadingIndicator('#articleDetailView', { left: "38%", top: "60px;" });

            articleDataFactory.getById(id)
                .success(function (data) {
                    vm.article = data;
                    // display html content
                    vm.htmlBody = $sce.trustAsHtml(data.body);
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#articleDetailView');
                });
        }

        // check if article is saved
        function isSaved(token, model) {
            // user is not authenticated if toke doesn't exist
            if (!token || token == '') {
                vm.isSaved = false;
                return;
            }

            // check if it's saved
            userFavoriteDataFactory.isSaved(token, model)
                .success(function (data) {
                    if (data) {
                        vm.isSaved = true;
                    }
                    else {
                        vm.isSaved = false;
                    }
                });
        }

        // save article into user favorites
        function saveArticle(token, model) {
            // save article into user favorites
            userFavoriteDataFactory.save(token, model)
                .then(function successCallback(response) {
                    // save success                    
                    vm.isSaved = true;

                }, function errorCallback(response) {
                    // if user is not authencited
                    if (response.status === 401) {
                        // direct into login page
                        // redirect into login page with return url
                        utilService.redirectToLoginPage('articles', articleId);
                    }
                });
        }

        // remove article from user favorites
        function removeArticle(token, model) {
            // save article into user favorites
            userFavoriteDataFactory.remove(token, model.saveItemType, model.keyId)
                .then(function successCallback(response) {
                    // remove success                 
                    vm.isSaved = false;

                }, function errorCallback(response) {
                    // if user is not authencited
                    if (response.status === 401) {
                        // direct into login page
                        // redirect into login page with return url
                        utilService.redirectToLoginPage('articles', articleId);
                    }
                });
        }
    }
})(angular.module('gbmono'));