/*
    jquery plugin service, init jquery component within controller 
*/
(function (module) {
    svr.$inject = ['$timeout'];

    module.service('pluginService', svr);

    function svr($timeout) {
        return {
            slider: slider
        };

        function slider() {
            // call $timeout to make sure dom is ready
            $timeout(function () {
                if ($('#slider').length) {
                    var owl = $("#slider");

                    // init owl carousel plugin
                    $("#slider").owlCarousel({
                        autoPlay: 3000,
                        items: 4, //10 items above 1000px browser width
                        itemsDesktopSmall: [900, 3], // betweem 900px and 601px
                        itemsTablet: [600, 3], //2 items between 600 and 0
                        itemsMobile: [500, 2] // itemsMobile disabled - inherit from itemsTablet option
                    });
                }
            });
        }
    }
})(angular.module('gbmono'));