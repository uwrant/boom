(function () {
    "use strict";
    var app = angular.module('boom');

    app.factory("presentationStorage", function () {
        'use strict';

        var STORAGE_ID = "vortrag-storage";

        return {
            get: function () {
                return JSON.parse(localStorage.getItem(STORAGE_ID) || '[]');
            },

            put: function (presentations) {
                localStorage.setItem(STORAGE_ID, JSON.stringify(presentations));
            }
        };
    });
})();