/*
    validation service, validate data object
*/

(function (module) {
    svr.$inject = ['$timeout'];

    module.service('validator', svr);

    function svr($timeout) {
        // return svr class
        return {
            isValidProduct:isValidProduct
        };

        function isValidProduct(product) {
            // todo:
            if (!product.primaryName || product.primaryName === '') {
                return false;
            }

            return true;
        }
    }
})(angular.module('gbmono'));