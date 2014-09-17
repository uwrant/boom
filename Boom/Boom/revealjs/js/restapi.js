(function () {
    var app = angular.module('boom');

    app.factory("Backlogs", function ($resource) {
        'use strict';

        return $resource("/backlogs/:id", {}, {
            save: { method: 'PUT' },
            create: { method: 'POST' },
        });
    });

    app.factory("Options", function ($resource) {
        'use strict';

        return $resource("/backlogs/:backlogId/options/:optionId", {}, {
            update: {
                method: 'PUT'
            }
        });
    });
})();