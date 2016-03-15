/*
    layout controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope'];

    // create controller
    module.controller('layoutController', ctrl);

    // controller body
    function ctrl($scope) {

        // hide the right sidebar (tree) when clicking on the area except the rigith sidebar itself
        $scope.hideRightSidebar = function ($event) {
            if (($($event.target).closest('#rightSidebar').length === 0) && ($($event.target).closest('#tree-trigger').length === 0)) {
                if ($('#rightSidebar').hasClass("toggled")) {
                    $('#rightSidebar').toggleClass('toggled');
                }
            }
        };

    }
})(angular.module('gbmono'));

/*
    header controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope'];

    // create controller
    module.controller('headerController', ctrl);

    // controller body
    function ctrl($scope) {
        // toggle right side bar
        $scope.toggleSidebar = function () {
            $('#rightSidebar').toggleClass('toggled');
        };
    }
})(angular.module('gbmono'));


/*
    category treeview controlelr 
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['$scope', 'utilService', 'categoryDataFactory'];

    // create controller
    module.controller('categoryTreeController', ctrl);

    // controller body
    function ctrl($scope, utilService, categoryDataFactory) {
        // tree web api url
        var rootApiUrl = gbmono.api_site_prefix.category_api_url + '/treeview';

        // !Important: loadTree is executed in angular "onload" event to make use DOM is ready when this methid is called
        $scope.loadTree = function () {
            bindTree();
        };

        function bindTree() {
            // get token
            var token = utilService.getToken();

            // load category data
            var categories = new kendo.data.HierarchicalDataSource({
                type: "json",
                schema: {
                    model: {
                        hasChildren: "hasChildren",
                        id: "id"
                    }
                },
                transport: {
                    read: {
                        url: function (options) {
                            return rootApiUrl + '/' + (options.id ? options.id : '');
                        },
                        headers: { Authorization: 'Bearer ' + token } // bear token
                    }
                },
                error: function (e) {
                    console.log(e);
                    // TODO: if it's unauthorized error
                    if (e.errorThrown == 'Unauthorized') {
                        // redirect into login page
                        window.location = gbmono.html_app + '/login.html';
                    }
                    else if (e.errorThrown == '') {
                        // forbidden error
                        // redirec into unauthorized page
                    }

                }
            });

            // init kendo treeview instance
            $("#treeview").kendoTreeView({
                dataSource: categories,
                dataTextField: ["name"],
                // dataImageUrlField: "image",
                dataUrlField: "linksTo",
                theme: "flat",
                dataBound: function (e) { // data bound event handler
                    //// we need attach the tooltip into the measure point nodes
                    //jQuery('.k-image', e.node).each(function (index, img) {
                    //    // retreive the link text
                    //    var tooltipText = $(img).parent().text();
                    //    // add tootip class
                    //    $(img).parent().addClass("tooltip-success");
                    //    // set tooltip
                    //    $(img).parent().tooltip({ placement: 'top', title: tooltipText });
                    //});
                }
            });
        }
    }

})(angular.module('gbmono'));


/*
   login controller
*/
(function (module) {
    // inject the controller params
    ctrl.$inject = ['pluginService', 'utilService', 'accountDataFactory'];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl(pluginService, utilService, accountDataFactory) {
        var vm = this;
        // login model
        vm.identity = {};
        // is processing
        vm.isDataLoading = false;

        // viw handler
        vm.login = function () {
            login(vm.identity.userName, vm.identity.password);
        };

        // page init
        init();

        function init() {

        }

        function login(userName, password) {
            // disable login button when data is loading
            vm.isDataLoading = true;
            // authenticate
            accountDataFactory.login(userName, password)
                .success(function (data) {
                    // todo: save token & user name?
                    utilService.saveToken(data.access_token);
                    utilService.saveUserName(data.userName);

                    // redirect into dashboard or return url
                    // get return url
                    var returnUrl = getReturnUrl();
                    // redirect 
                    window.location = returnUrl;
                })
                .error(function (error) {
                    // login failed
                    // show up error
                    pluginService.notify(error, 'error');

                    // enable login button
                    vm.isDataLoading = false;
                });
        }

        // extract the return url if it exists
        function getReturnUrl() {
            var url = window.location.href;
            var index = url.toLowerCase().indexOf('returnurl');
            if (index === -1) {
                // no return url then redirect into home page
                return gbmono.html_app + '/#/';
            } else {
                // extract the route from the return url
                var route = url.substring(index + 10);

                // redirect to the route
                return gbmono.html_app + '/#/' + route;
            }
        }
    }
})(angular.module('gbmono'));

