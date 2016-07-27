/*
    article list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location',
                    '$filter',
                    '$timeout',
                    'utilService',
                    'pluginService',
                    'articleDataFactory'];

    // create controller
    module.controller('articleListController', ctrl);

    // controller body
    function ctrl($location,
                  $filter,
                  $timeout,
                  utilService,
                  pluginService,
                  articleDataFactory) {
        var vm = this;
        // articles
        // marketing
        vm.marketingArticles = [];
        // shops
        vm.shopArticles = [];
        // products
        vm.productArticles = [];
        // others
        vm.others = [];

        // ranking
        vm.rankingArticles = [];

        // article image root path
        vm.articleImgRoot = gbmono.img_article_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;


        init(); // page init 

        function init() {
            // date range, last 14 days
            var to = $filter('date')(new Date(), 'yyyy-MM-dd')
            var from = $filter('date')(new Date().setDate(new Date().getDate() - 14), 'yyyy-MM-dd');

            // get articles
            getArticlesByDate(from, to);

            // get ranking articles
            getArticleByRanking();
        }

        // get articles by date
        function getArticlesByDate(from, to) {
            // loading status
            pluginService.showDataLoadingIndicator('#articles', { left: "38%", top: "60px;" });

            articleDataFactory.getByDate(from, to)
                .success(function (data) {
                    // group by type
                    for (var i = 0, len = data.length; i < len; i++) {
                        if (data[i].articleTypeId == 1) {
                            vm.marketingArticles.push(data[i]);
                        }
                        else if (data[i].articleTypeId == 2) {
                            vm.shopArticles.push(data[i]);
                        }
                        else if (data[i].articleTypeId == 3) {
                            vm.productArticles.push(data[i]);
                        }
                        else if (data[i].articleTypeId == 4) {
                            vm.others.push(data[i]);
                        }
                    }
                    // close data loading
                    pluginService.closeDataLoadingIndicator('#articles');

                    // enable tab
                    initTab();
                });
        }

        // get ranking articles
        function getArticleByRanking() {
            articleDataFactory.getByRanking()
                .success(function (data) {
                    vm.rankingArticles = data;
                });
        }

        function initTab() {
            $timeout(function () {
                $(".nav>ul>li").on("click", function (e) {
                    var currentBtn = $(e.currentTarget);
                    if (currentBtn.hasClass("selected")) {
                        return false;
                    }

                    var chooseItem = currentBtn.attr("chooseitem");


                    $(".news-list").removeClass("selected");
                    switch (chooseItem) {
                        case "company":
                            $(".news-list.company").addClass("selected");
                            break;
                        case "store":
                            $(".news-list.store").addClass("selected");
                            break;
                        case "product":
                            $(".news-list.product").addClass("selected");
                            break;
                        case "other":
                            $(".news-list.other").addClass("selected");
                            break;
                    }

                    $(".nav>ul>li").removeClass("selected");
                    currentBtn.addClass("selected");

                });
            });
        }
    }
})(angular.module('gbmono'));