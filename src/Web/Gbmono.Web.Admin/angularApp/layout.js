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
    ctrl.$inject = ['$scope', 'categoryDataFactory'];

    // create controller
    module.controller('categoryTreeController', ctrl);

    // controller body
    function ctrl($scope, categoryDataFactory) {
        // tree web api url
        var rootApiUrl = gbmono.api_site_prefix.category_api_url + '/treeview';

        // !Important: loadTree is executed in angular "onload" event to make use DOM is ready when this methid is called
        $scope.loadTree = function () {
            bindTree();
        };

        function bindTree() {
            // plant area data, root data
            var plantArea = new kendo.data.HierarchicalDataSource({
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
                        }
                        // headers: { Authorization: 'Bearer ' + window.localStorage.getItem(metsys.LOCAL_STORAGE_TOKEN_KEY) } // bear token
                    }
                },
                error: function (e) {
                    // TODO: if it's unauthorized error
                    if (e.errorThrown == 'Unauthorized') {

                    }
                    else if (e.errorThrown == '') {
                        // forbidden error
                        // redirec into unauthorized page
                    }

                }
            });

            // init kendo treeview instance
            $("#treeview").kendoTreeView({
                dataSource: plantArea,
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
    ctrl.$inject = ['pluginService', 'accountDataFactory'];

    // create controller
    module.controller('loginController', ctrl);

    // controller body
    function ctrl(pluginService, accountDataFactory) {
        var vm = this;
        // login model
        vm.identity = {};

        // page init
        init();

        function init() {

        }

        function login(userName, password) {
            accountDataFactory.login(userName, password)
                .success(function () {

                })
                .error(function (error) {
                    // login failed

                });
        }

    }
})(angular.module('gbmono'));

