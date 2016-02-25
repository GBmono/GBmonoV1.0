/*
    jquery plugin service, init jquery component within controller 
*/
(function (module) {
    svr.$inject = ['$timeout'];

    module.service('pluginService', svr);

    function svr($timeout) {
        return {
            slider: slider,
            productDetailGallery: productDetailGallery
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

        function productDetailGallery(confDetailSwitch) {
            $timeout(function () {
                $('#productMain .thumb:first').addClass('active');
                timer = setInterval(autoSwitch, confDetailSwitch);
                $("#productMain .thumb").click(function (e) {

                    switchImage($(this));
                    clearInterval(timer);
                    timer = setInterval(autoSwitch, confDetailSwitch);
                    e.preventDefault();
                }
                );
                $('#productMain #mainImage').hover(function () {
                    clearInterval(timer);
                }, function () {
                    timer = setInterval(autoSwitch, confDetailSwitch);
                });
                function autoSwitch() {
                    var nextThumb = $('#productMain .thumb.active').closest('div').next('div').find('.thumb');
                    if (nextThumb.length == 0) {
                        nextThumb = $('#productMain .thumb:first');
                    }
                    switchImage(nextThumb);
                }

                function switchImage(thumb) {

                    $('#productMain .thumb').removeClass('active');
                    var bigUrl = thumb.attr('href');
                    thumb.addClass('active');
                    $('#productMain #mainImage img').attr('src', bigUrl);
                }
            });
        }
    }
})(angular.module('gbmono'));