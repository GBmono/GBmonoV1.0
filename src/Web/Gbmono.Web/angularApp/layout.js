/*
    layout controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = [];

    // create controller
    module.controller('layoutController', ctrl);

    // controller body
    function ctrl() {

    }
})(angular.module('gbmono'));


/*
   header block controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['categoryDataFactory', 'pluginService'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl(categoryDataFactory, pluginService) {
        var vm = this; // controller reference
        // for ui display purpose
        vm.topCatesCol1 = [];
        vm.topCatesCol2 = [];
        vm.topCatesCol3 = [];
        vm.topCatesCol4 = [];

        // page init 
        init();

        function init() {
            // get top categories
            getTopCategories();
        }

        function getTopCategories() {
            categoryDataFactory.getTopCates()
                .success(function (data) {
                    //// nav header menu sliding 
                    //pluginService.menuSliding();

                    // todo: 
                    vm.topCatesCol1 = fillCategories(0, data);
                    vm.topCatesCol2 = fillCategories(4, data);
                    vm.topCatesCol3 = fillCategories(8, data);
                    vm.topCatesCol4 = fillCategories(12, data);
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
   footer block controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pageDataFactory'];

    // create controller
    module.controller('footerController', ctrl);

    // controller body
    function ctrl(pageDataFactory) {
        // controller reference
        var vm = this;
        // footer data items
        vm.footerDataItems = [];

        // page init
        init();

        function init() {
            // load menu data from json file
            getFooterData();
        }

        function getFooterData() {
            pageDataFactory.getFooterData()
                .success(function (data) {
                    vm.footerDataItems = data;
                });
        }
    }
})(angular.module('gbmono'));





