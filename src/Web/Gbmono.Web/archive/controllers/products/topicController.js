/*
    topic list controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'pageDataFactory'];

    // create controller
    module.controller('topicController', ctrl);

    // controller body
    function ctrl(pluginService, pageDataFactory) {
        var vm = this;
        //topic items
        vm.items = [];

        init(); // page init 

        function init() {
            getTopicItems();
        }

        function getTopicItems() {
            pageDataFactory.getTopicItems()
                .success(function (data) {
                    vm.items = data;
                });
        }

    }
})(angular.module('gbmono'));