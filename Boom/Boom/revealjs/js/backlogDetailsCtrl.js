(function () {
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, presentationStorage) {
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
})();