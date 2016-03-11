/*
    brand list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'brandDataFactory'];

    // create controller
    module.controller('brandListController', ctrl);

    // controller body
    function ctrl(pluginService, brandDataFactory) {
        var vm = this;
        // brands
        vm.brands = [];
        // alphabet filter
        vm.alphabets = ['ア','カ','サ','タ', 'ナ', 'ハ', 'マ', 'ヤ', 'ラ', 'ワ', 'その他'];

        // view event handlers
        vm.showBrandDetail = function () {
            pluginService.modal('#brand');
        };

        init(); // page init 

        function init() {
            // load brands
            getBrands();
        }

        function getBrands() {
            brandDataFactory.getAll()
                .success(function (data) {
                    vm.brands = data;
                });
        }
    }
})(angular.module('gbmono'));