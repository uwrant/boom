(function () {
    var app = angular.module('boom');

    app.factory("backlogService", function () {
        'use strict';

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