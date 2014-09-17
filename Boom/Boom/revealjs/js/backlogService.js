(function () {
    "use strict";
    var app = angular.module('boom');

    app.factory("backlogService", function () {
        
        var selectedBacklog = undefined;

        return {
            getSelectedBacklog: function () {
                return selectedBacklog;
            },

            setSelectedBacklog: function (backlog) {
                selectedBacklog = backlog;
            }
        };
    });
})();