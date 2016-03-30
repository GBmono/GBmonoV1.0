/*
    category list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'categoryDataFactory'];

    // create controller
    module.controller('categoryListController', ctrl);

    // controller body
    function ctrl(pluginService, categoryDataFactory) {
        var vm = this;
        // categories
        vm.categories = [];

        init(); // page init 

        function init() {
            getCategories();
        }

        function getCategories() {
            // load data
            categoryDataFactory.getAll()
                .success(function (data) {
                    vm.categories = data;
                });
        }
    }
})(angular.module('gbmono'));