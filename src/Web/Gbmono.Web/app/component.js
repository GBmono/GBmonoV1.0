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
        // devided into 4 collections map into 4 vertical list elements
        vm.topCatesCol1 = [];
        vm.topCatesCol2 = [];
        vm.topCatesCol3 = [];
        vm.topCatesCol4 = [];

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
                    vm.topCatesCol1 = fillCategories(0, data);
                    vm.topCatesCol2 = fillCategories(4, data);
                    vm.topCatesCol3 = fillCategories(8, data);
                    vm.topCatesCol4 = fillCategories(12, data);
                    //console.log(vm.topCatesCol1);
                });
        }

        function fillCategories(startIndex, data) {
            var result = [];
            for (var i = startIndex; i < startIndex + 4; i++) {
                if (i > data.length) {
                    break;
                }
                result.push(data[i]);
            }

            return result;
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