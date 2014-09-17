(function () {
    'use strict';
    var app = angular.module('boom');

    app.factory("surveyService", function () {
       
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