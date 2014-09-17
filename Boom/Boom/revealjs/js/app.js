(function (global) {
    var app = angular.module('boom', ['ngResource']);
    
    app.run(function ($rootScope) {
        global.$rootScope = $rootScope;
    });

})(window);