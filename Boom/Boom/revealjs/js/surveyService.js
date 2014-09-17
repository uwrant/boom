(function () {
    var app = angular.module('boom');

    app.factory("surveyService", function () {
        'use strict';

        var options = undefined;

        return {

            getOptions: function () {
                return options;
            },

            setOptions: function (newOptions) {
                options = angular.copy(newOptions);
            }

        };
    });
})();