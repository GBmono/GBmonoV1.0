/*
    profile controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$location', 'utilService'];

    // create controller
    module.controller('articleIndexController', ctrl);

    // controller body
    function ctrl($location, utilService) {
        var vm = this;
        
        // product image root path
        vm.imgRoot = gbmono.img_root_path;
        // paging
        vm.paging = { pageIndex: 1, pageSize: 12 };
        // indicate if all data is loaded
        vm.isAllDataLoaded = false;


        init(); // page init 

        function init() {

        }



    }
})(angular.module('gbmono'));