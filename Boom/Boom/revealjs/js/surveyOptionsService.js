(function () {
    'use strict';
    var app = angular.module('boom');

    app.factory("SurveyOptionsService", function () {
       
        var options = undefined;
        var survey = undefined;

        return {

            getOptions: function () {
                return options;
            },

            setOptions: function (newOptions) {
                options = angular.copy(newOptions);
            },

            getCurrentSurvey: function () {
                return survey;
            },

            setCurrentSurvey: function (currentSurvey) {
                survey = currentSurvey;
            }

        };
    });
})();