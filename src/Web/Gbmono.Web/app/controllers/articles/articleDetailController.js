/*
    article detail controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$sce',
                    'utilService',
                    'pluginService',
                    'articleDataFactory'];

    // create controller
    module.controller('articleDetailController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $sce,
                  utilService,
                  pluginService,
                  articleDataFactory) {
        var vm = this;
        // article
        vm.article = {};
        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;

        var articleId = $routeParams.id ? parseInt($routeParams.id) : 0;

        init(); // page init 

        function init() {
            if (articleId != 0) {
                getArticleById(articleId);
            }
        }
        
        function getArticleById(id) {
            // loading progress
            pluginService.showDataLoadingIndicator('#articleDetailView', { left: "38%", top: "60px;" });

            articleDataFactory.getById(id)
                .success(function (data) {
                    vm.article = data;
                    vm.htmlBody = $sce.trustAsHtml(data.body);

                    // close data loading
                    pluginService.closeDataLoadingIndicator('#articleDetailView');
                });
        }
    }
})(angular.module('gbmono'));