/*
    jquery plugin service, init jquery component within controller 
*/
(function (module) {
    svr.$inject = ['$timeout'];

    module.service('pluginService', svr);

    function svr($timeout) {
        return {
            slider: slider,
            snap: snap,
            tab:tab,
            productDetailGallery: productDetailGallery,
            showDataLoadingIndicator: showDataLoadingIndicator,
            closeDataLoadingIndicator: closeDataLoadingIndicator
            // modal: modal
        };

        // silder effect
        function slider() {
            // call $timeout to make sure dom is ready
            $timeout(function () {
                var mainVisualCollection = $("#visualList");
                mainVisualCollection.owlCarousel({
                    items: 1, //10 items above 1000px browser width
                    itemsDesktop: [1200, 1], //5 items between 1000px and 901px
                    itemsDesktopSmall: [1100, 1], // betweem 900px and 601px
                    itemsTablet: [1000, 1], //2 items between 600 and 0
                    itemsMobile: false,
                    autoPlay: true,
                    lazyLoad: true,
                    pagination: true
                });
          
            });
        }

        // snap slider
        function snap() {
            $timeout(function () {
                /*底部轮播图片*/
                var snapList = $("#snapList");
                snapList.owlCarousel({
                    items: 4, //10 items above 1000px browser width
                    itemsDesktop: [840, 4], //5 items between 1000px and 901px
                    itemsDesktopSmall: [740, 3], // betweem 900px and 601px
                    itemsTablet: [600, 2], //2 items between 600 and 0
                    itemsMobile: false,
                    autoPlay: true,
                    lazyLoad: true,
                    pagination: false
                });
            });
        }

        // product thumbnail gallery
        function productDetailGallery() {
            $timeout(function () {
                $(".thumb").hover(function (e) {
                    const currentBtn = $(e.currentTarget);
                    $(".thumb").removeClass("active");
                    currentBtn.addClass("active");
                    var bigImg = currentBtn.attr("bigimg");
                    $(".detail-img img").attr("src", bigImg);
                }, function () {
                    //$(".thumb").removeClass("active");
                });
            });
        }

        // boostrap tab
        function tab() {
            // disable the default behavior
            $timeout(function () {
                $(".product-tab").on("click", function (e) {
                    var currentBtn = $(e.currentTarget);
                    $(".product-tab").removeClass("active");
                    currentBtn.addClass("active");
                    var dom = currentBtn.attr("chooseitem");

                    $(".tab-item-content").removeClass("active");
                    $("." + dom).addClass("active");

                });
            });                     
        }

        // data loading indicator
        function showDataLoadingIndicator(selector, position) {
            //if (!position) {
            //    position = { left: "10%", top: "0px" };
            //}

            //if (!position.left) {
            //    position.left = "10%";
            //}

            //if (!position.top) {
            //    position.top = "0px";
            //}

            // add div with progress indicator on the top of the current widget
            $(selector).append('<div class="widget-box-overlay">' +
                                '<div class="loading-icon" style="left:' + position.left + '; top:' + position.top + '">' +
                                '<img src="content/img/loading_1.gif" alt="loading"/>' +
                                '</div>' +
                            '</div>');
        }

        // close progess indicator
        function closeDataLoadingIndicator(selector) {
            // remove the indicator div
            $(selector).find('.widget-box-overlay').remove();
        }

        //function modal(selector) {
        //    $(selector).modal();
        //}

    }
})(angular.module('gbmono'));