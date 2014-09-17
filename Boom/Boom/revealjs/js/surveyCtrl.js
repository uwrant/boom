(function () {
    var app = angular.module('boom');

    app.controller("SurveyCtrl", function BacklogCtrl($scope, SurveyServiceMock, backlogService, surveyService, revealService, toaster) {
        'use strict';

        var SurveyService = SurveyServiceMock;

        var vm = this;
        vm.qrCodeText = "''";

        var checkPreConditions = function () {
            if (typeof backlogService.getSelectedBacklog() === 'undefined') {
                toaster.pop('error', "", "Please select a backlog!", 10000);
                revealService.navigateToSlide("BacklogSlide");
                return false;
            }

            if (surveyService.getOptions() == null || typeof surveyService.getOptions() === 'undefined') {
                toaster.pop('error', "", "Please select at least one option for the survey!", 10000);
                revealService.navigateToSlide("BacklogContentSlide");
                return false;
            }

            return true;
        }

        var createNewSurveyFromSelectedBacklog = function () {
            var selectedBacklog = backlogService.getSelectedBacklog();
            
            vm.survey = SurveyService.create({
                CreationDate: new Date(),
                StartDate: new Date(),
                Options: surveyService.getOptions()
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