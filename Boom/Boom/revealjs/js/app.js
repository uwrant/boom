(function (global) {
    "use strict";
    var app = angular.module('boom', ['ngResource', 'ja.qr']);
    
    app.run(function ($rootScope) {
        global.$rootScope = $rootScope;
    });

})(window);