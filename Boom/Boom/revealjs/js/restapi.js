'use strict';
(function () {
    var app = angular.module('boom');

    app.factory("BacklogsService", function ($resource) {
        return $resource("/backlogs/:id", {}, {
            save: { method: 'PUT' },
            create: { method: 'POST' }
        });
    });

    app.factory("OptionsService", function ($resource) {
        return $resource("/backlogs/:backlogId/options/:optionId", {}, {
            update: {
                method: 'PUT'
            }
        });
    });

    app.factory("OptionsServiceMock", function () {
        return {
            query: function () {
                return [
                    { Id: 1, Name: "Option 1" },
                    { Id: 2, Name: "Option 2" },
                    { Id: 3, Name: "Option 3" },
                ];
            }
        }
    })
})();