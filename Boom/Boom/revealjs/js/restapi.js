(function () {
    var app = angular.module('boom');

    app.factory("BacklogsService", function ($resource) {
        'use strict';

        return $resource("/backlogs/:id", {}, {
            save: { method: 'PUT' },
            create: { method: 'POST' }
        });
    });

    app.factory("OptionsService", function ($resource) {
        'use strict';

        return $resource("/backlogs/:backlogId/options/:optionId", {}, {
            update: {
                method: 'PUT'
            }
        });
    });
})();