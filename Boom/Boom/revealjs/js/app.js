(function (global) {
    "use strict";
    var app = angular.module('boom', ['ngResource', 'ja.qr', 'toaster']);
    
    app.run(function ($rootScope) {
        global.$rootScope = $rootScope;
    });

})(window);