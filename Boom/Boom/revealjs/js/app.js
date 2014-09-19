(function (global) {
    "use strict";
    var app = angular.module('boom', ['ngResource', 'ja.qr', 'toaster']);
    
    app.run(function ($rootScope, toaster) {
        global.$rootScope = $rootScope;

        $rootScope.$on("slidechanged", function (event, data) {
            //toaster.clear();
        });
    });

})(window);