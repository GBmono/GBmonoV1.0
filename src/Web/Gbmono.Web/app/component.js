/*
   header component controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl() {

        init();

        function init() {
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
    }

})(angular.module('gbmono'));


/*
   ranking component controller
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
                    vm.products = data; console.log(data);
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