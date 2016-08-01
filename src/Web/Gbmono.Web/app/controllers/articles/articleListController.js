/*
    article list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$routeParams',
                    '$filter',
                    '$timeout',
                    'utilService',
                    'pluginService',
                    'articleDataFactory'];

    // create controller
    module.controller('articleListController', ctrl);

    // controller body
    function ctrl($routeParams,
                  $filter,
                  $timeout,
                  utilService,
                  pluginService,
                  articleDataFactory) {
        var vm = this;
        // articles
        vm.articles = [];

        // ranking
        vm.rankingArticles = [];

        // selected article type name
        vm.articleTypeName = '';

        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };

        // indicate if loading starts
        vm.isLoadingStarts = false;
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;

        // retreive article type id from url
        // default type id value is 1
        var articleTypeId = $routeParams.articleTypeId ? parseInt($routeParams.articleTypeId) : 1;

        init(); // page init 

        function init() {
            // todo: process invalid type id value
            getArticleTypeTitle(articleTypeId);

            //// date range, last 14 days
            //var to = $filter('date')(new Date(), 'yyyy-MM-dd')
            //var from = $filter('date')(new Date().setDate(new Date().getDate() - 14), 'yyyy-MM-dd');

            // get articles
            getArticles(articleTypeId,
                              vm.paging.pageIndex,
                              vm.paging.pageSize);

            // get ranking articles
            getArticleByRanking();

            // attach scroll event handler
            angular.element(window).unbind('scroll').scroll(function () {
                var top = angular.element(window).scrollTop();
                var bottom = angular.element(document).height() - angular.element(window).height() - 50;
                // when it still has more data to be loaded
                if (top > bottom && !vm.isAllDataLoaded) {
                    // avoild sending multiple requests
                    if (!vm.isLoadingStarts) {
                        // load more products when scrolling at the bottom
                        getArticlesByDate(articleTypeId,
                                          vm.paging.pageIndex, // first page
                                          vm.paging.pageSize); // page size
                    }
                }
            });
        }

        // get articles by date
        function getArticles(articleTypeId, pageIndex, pageSize) {
            // data requesting starts
            vm.isLoadingStarts = true;

            // call api
            articleDataFactory.getByType(articleTypeId, pageIndex, pageSize)
                .success(function (data) {
                    // append result into current articles collection
                    vm.articles = vm.articles.concat(data);
                    // add page index
                    vm.paging.pageIndex++;
                    // if no more data
                    if (data.length < vm.paging.pageSize) {
                        // disable or hide the button
                        vm.isAllDataLoaded = true;
                    }
                  
                    // data requesting finishes
                    vm.isLoadingStarts = false;
                });
        }

        // get ranking articles
        function getArticleByRanking() {
            articleDataFactory.getByRanking()
                .success(function (data) {
                    vm.rankingArticles = data;
                });
        }

        // get article type title
        function getArticleTypeTitle(typeId) {
            if (typeId === 1) {
                vm.articleTypeName = '行业';
            }
            else if (typeId == 2) {
                vm.articleTypeName = '零售店';
            }
            else if (typeId == 3) {
                vm.articleTypeName = '产品';
            }
            else if (typeId == 4) {
                vm.articleTypeName = '其它';
            }
        }
    }
})(angular.module('gbmono'));