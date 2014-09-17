(function () {
    var app = angular.module('boom');

    app.controller("SurveyCtrl", function BacklogCtrl($scope, SurveyServiceMock, backlogService) {
        'use strict';

        var SurveyService = SurveyServiceMock;

        var vm = this;
        vm.qrCodeText = "''";

        var checkPreConditions = function () {
            var selectedBacklog = backlogService.getSelectedBacklog();
            if (typeof selectedBacklog == 'undefined') {
                console.error("Please select a backlog!");
                return false;
            }
            
            if (selectedBacklog.Options == null || typeof selectedBacklog.Options === 'undefined') {
                console.error("Please enter at least one option.");
                return false;
            }

            return true;
        }

        var createNewSurveyFromSelectedBacklog = function () {
            var selectedBacklog = backlogService.getSelectedBacklog();
            
            vm.survey = SurveyService.create({
                CreationDate: new Date(),
                StartDate: new Date(),
                Options: selectedBacklog.Options.filter(function(option) {
                    return !option.disabled;
                })
            }, function (data) {
                createQrCodeText();
            });
        };

        var createQrCodeText = function () {
            vm.qrCodeText = "'http://www.nba.com'"
        };

        $scope.$on("slidechanged:SurveyStartSlide", function (event, data) {
            if (checkPreConditions()) {
                createNewSurveyFromSelectedBacklog();
            }
        });

        vm.startSurvey = function () {
            vm.survey.State = 1; //Started
            vm.survey.$save();
        };
    });
})();