(function () {
    'use strict';
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
            },
            create: {
                method: 'POST'
            }
        });
    });

    app.factory("SurveyService", function ($resource) {
        return $resource("/surveys/:id", {}, {
            save: { method: 'PUT' },
            create: { method: 'POST' },
            start: { method: 'PATCH' }
        });
    });

    app.factory("ParticipantsService", function ($resource) {
        return $resource("/surveys/:surveyId/participants", {}, {});
    });

})();