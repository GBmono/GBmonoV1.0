/*
    jquery plugin service, init jquery component within controller 
*/
(function (module) {
    svr.$inject = ['$timeout'];

    module.service('pluginService', svr);

    function svr($timeout) {
        return {
            slider: slider,
            tab:tab,
            productDetailGallery: productDetailGallery,
            notify: notify,
            showDataLoadingIndicator: showDataLoadingIndicator,
            closeDataLoadingIndicator: closeDataLoadingIndicator,
            modal: modal
        };

        // silder effect
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

        // product thumbnail gallery
        function productDetailGallery(confDetailSwitch) {
            $timeout(function () {
                $('#productMain .thumb:first').addClass('active');
                // timer = setInterval(autoSwitch, confDetailSwitch);
                $("#productMain .thumb").click(function (e) {

                    switchImage($(this));
                    // clearInterval(timer);
                    // timer = setInterval(autoSwitch, confDetailSwitch);
                    e.preventDefault();
                });
                
                //$('#productMain #mainImage').hover(function () {
                //    // clearInterval(timer);
                //}, function () {
                //    // timer = setInterval(autoSwitch, confDetailSwitch);
                //});

                //function autoSwitch() {
                //    var nextThumb = $('#productMain .thumb.active').closest('div').next('div').find('.thumb');
                //    if (nextThumb.length == 0) {
                //        nextThumb = $('#productMain .thumb:first');
                //    }
                //    switchImage(nextThumb);
                //}

                function switchImage(thumb) {
                    // switch thumbnail img 
                    $('#productMain .thumb').removeClass('active');
                    var bigUrl = thumb.attr('href');
                    thumb.addClass('active');
                    $('#productMain #mainImage img').attr('src', bigUrl);
                }
            });
        }

        // boostrap tab
        function tab() {
            // disable the default behavior
            $timeout(function () {
                $('a[data-toggle="tab"]').click(function (e) {
                    e.preventDefault();
                });
            });
        }

        // $.growl notification
        function notify(response, type) {
            var msg = '';

            // if type is error
            // extract the acutal error message from object
            if (type === 'error') {
                if (response.data) {
                    if (response.data.message) {
                        msg = response.data.message;
                    } else if (response.data.error_description) {
                        msg = response.data.error_description;
                    }

                } else {
                    // generic error message
                    msg = 'Unexpected error has occurred. Please contact the administrator.';
                }
            }

            $.growl(
                {
                    icon: type === 'success' ? 'fa fa-check' : 'fa fa-exclamation-triangle',
                    title: type === 'success' ? ' Success!  ' : ' Error!  ',
                    message: msg,
                    url: ''
                },
                {
                    element: 'body',
                    type: type === 'error' ? 'danger' : 'success',
                    allow_dismiss: true,
                    //position: 'relative',
                    placement: {
                        from: 'top',
                        align: 'center'
                    },
                    offset: {
                        x: 20,
                        y: 85
                    },
                    spacing: 10,
                    z_index: 1031,
                    delay: 2500,
                    timer: 2000,
                    url_target: '_blank',
                    mouse_over: false,
                    animate: {
                        enter: 'animated fadeIn',
                        exit: 'animated fadeOut'
                    },
                    icon_type: 'class',
                    template: '<div data-growl="container" class="alert" role="alert">' +
                                    '<button type="button" class="close" data-growl="dismiss">' +
                                        '<span aria-hidden="true">&times;</span>' +
                                        '<span class="sr-only">Close</span>' +
                                    '</button>' +
                                    '<span data-growl="icon"></span>&nbsp;&nbsp;' +
                                    // '<span data-growl="title"></span>' +
                                    '<span data-growl="message"></span>' +
                                    '<a href="#" data-growl="url"></a>' +
                                '</div>'
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
                                '<img src="/gbmono/content/img/loading_1.gif" alt="loading"/>' +
                                '</div>' +
                            '</div>');
        }

        // close progess indicator
        function closeDataLoadingIndicator(selector) {
            // remove the indicator div
            $(selector).find('.widget-box-overlay').remove();
        }

        function modal(selector) {
            $(selector).modal();
        }
    }
})(angular.module('gbmono'));