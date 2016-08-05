/* data loading indicator */
(function (module) {
    // create directive
    module.directive('loadingIndicator', loadingIndicator);

    function loadingIndicator() {
        // directive class
        var directive = {
            restrict: 'A',
            template: '<div class="curtain"></div>' +
                          '<div class="curtain-content">' +
                               '<div>' +
                                    '<img src="/content/img/loading_3.gif" alt="Loading" />' +
                               '</div>' +
                          '</div>',
            link: link
        };

        return directive;

        // link  function
        function link($scope, element, attrs) {            
            // listener on the vaue of the mi-in-progress 
            attrs.$observe('dataLoading', function (val) {
                console.log(val);
                if (val == 'true') {
                    element.css('display', 'block'); // display in progress window
                } else {
                    element.css('display', 'none'); // close in progress window
                }
            });
        }
    }
})(angular.module('gbmono'));


