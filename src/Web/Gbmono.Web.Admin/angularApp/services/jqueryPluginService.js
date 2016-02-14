/*
    jquery plugin service, init jquery component
*/

(function (module) {
    svr.$inject = ['$timeout'];

    module.service('pluginService', svr);

    function svr($timeout){
        // return svr class
        return {
            notify: notify
        };

        // $.growl
        function notify(message, type) {
            var msg = message;

            // if type is error
            // extract the acutal error message from object
            if (type === 'error') {
                if (message) {
                    if (message.message) {
                        msg = message.message;
                    } else if (message.error_description) {
                        msg = message.error_description;
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
                    placement: {
                        from: 'top',
                        align: 'right'
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
                                    '<span data-growl="icon"></span>' +
                                    '<span data-growl="title"></span>' +
                                    '<span data-growl="message"></span>' +
                                    '<a href="#" data-growl="url"></a>' +
                                '</div>'
                });
        }

    }
})(angular.module('gbmono'));