(function (global) {
    var app = angular.module('boom', ['ngResource']);
    
    app.run(function ($rootScope) {
        global.$rootScope = $rootScope;
    });

    app.controller("VortragCtrl", function VortragCtrl($scope, presentationStorage) {
        'use strict';

        var presentations = $scope.presentations = presentationStorage.get();

        $scope.newPresentation = { speaker: "", title: "", disabled: false };
        $scope.disabledFilter = { disabled: true };

        $scope.$watch('presentations', function (newValue, oldValue) {
            if (newValue !== oldValue) { // This prevents unneeded calls to the local storage
                presentationStorage.put(presentations);
            }
        }, true);

        $scope.addPresentation = function () {
            var newPresentation = $scope.newPresentation;
            if (!newPresentation.speaker.length || !newPresentation.title.length) {
                return;
            }

            presentations.push(newPresentation);

            $scope.newPresentation = { speaker: "", title: "", disabled: false };
        }

        $scope.removePresentation = function (presentation) {
            var index = presentations.indexOf(presentation);
            if (index > -1) {
                presentations.splice(index, 1);
            }
        }

        $scope.notNowPresentation = function (presentation) {
            presentation.disabled = true;
        }

        $scope.nowPresentation = function (presentation) {
            presentation.disabled = false;
        }

        $scope.updateVotes = function (presentation) {
            presentation.votes = presentation.newVotes;
        }
    });
    

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
})(window);