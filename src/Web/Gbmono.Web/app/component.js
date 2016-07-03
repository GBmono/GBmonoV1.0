/*
   header component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['categoryDataFactory'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl(categoryDataFactory) {        
        var vm = this;
        // product top categories
        vm.topCates = [];

        init();

        function init() {
            getProductTopCategories();

            // scroll
            window.addEventListener("scroll", function (event) {
                const pageTopEl = $("#pageTop");
                const headerDomEl = $("#menuHeader");
                var scrollTop = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
                if (scrollTop > 0) {
                    pageTopEl.show();
                    headerDomEl.addClass("min-header");
                }
                else {
                    headerDomEl.removeClass("min-header");
                    pageTopEl.hide();
                }
            });
        }

        function getProductTopCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    vm.topCates = data;
                });
        }
    }

})(angular.module('gbmono'));


/*
   ranking product list component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['productDataFactory'];

    // create controller
    module.controller('rankingController', ctrl);

    // controller body
    function ctrl(productDataFactory) {
        var vm = this;
        // products by ranking
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        init();

        function init() {
            // load products bu ranking
            getRankingProducts();
        }
    
        function getRankingProducts() {
            productDataFactory.getByRanking()
                .success(function (data) {
                    vm.products = data; 
                });
        }
    }

})(angular.module('gbmono'));

/*
   recomended product list component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['productDataFactory', 'pluginService'];

    // create controller
    module.controller('recommendController', ctrl);

    // controller body
    function ctrl(productDataFactory, pluginService) {
        var vm = this;
        // products 
        vm.products = [];
        // product image root path
        vm.imgRoot = gbmono.img_root_path;

        init();

        function init() {
            // load recommended products
            getRecommendedProducts();
        }

        function getRecommendedProducts() {
            // todo: update the data method
            productDataFactory.getByCategory(38, 1, 8)
                .success(function (data) {
                    vm.products = data;
                    // init the jquery slider
                    // after data is retreived
                    pluginService.snap();
                });
        }
    }

})(angular.module('gbmono'));

/*
   footer component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('footerController', ctrl);

    // controller body
    function ctrl() {

    }
})(angular.module('gbmono'));