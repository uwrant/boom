(function () {
    var app = angular.module('boom');

    app.controller("SurveyCtrl", function BacklogCtrl($scope, SurveyService, backlogService, surveyService, revealService, toaster, $location) {
        'use strict';

        var vm = this;
        vm.qrCodeText = "''";

        var checkPreConditions = function () {
            if (typeof backlogService.getSelectedBacklog() === 'undefined') {
                toaster.pop('error', "", "Please select a backlog!", 10000);
                revealService.navigateToSlide("BacklogSlide");
                return false;
            }

            if (surveyService.getOptions() == null || typeof surveyService.getOptions() === 'undefined' || surveyService.getOptions().length == 0) {
                toaster.pop('error', "", "Please select at least one option for the survey!", 10000);
                revealService.navigateToSlide("BacklogContentSlide");
                return false;
            }

            return true;
        }

        var createNewSurveyFromSelectedBacklog = function () {
            var selectedBacklog = backlogService.getSelectedBacklog();
            
            vm.survey = SurveyService.create({
                Name: "SurveyName is not defined yet....",
                CreationDate: new Date(),
                Options: surveyService.getOptions()
            }, function (data) {
                createQrCodeText();
            }, function () {
                toaster.pop('error', "", "Error creating the survey!", 10000);
            });
        };

        var createQrCodeText = function () {
            vm.qrCodeText = "http://" + $location.host() + ":" + $location.port() + "/surveys/" + vm.survey.Id;
        };

        $scope.$on("slidechanged:SurveyStartSlide", function (event, data) {
            if (checkPreConditions()) {
                createNewSurveyFromSelectedBacklog();
            }
        });

        vm.startSurvey = function () {
            vm.survey.StartDate = new Date(); //Started
            vm.survey.$save(function () { }, function () {
                toaster.pop('error', "", "Error starting the survey!", 10000);
            });
        };
    });
})();