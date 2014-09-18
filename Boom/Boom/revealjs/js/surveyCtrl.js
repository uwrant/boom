(function () {
    var app = angular.module('boom');

    app.controller("SurveyCtrl", function BacklogCtrl($scope, SurveyService, backlogService, SurveyOptionsService, ParticipantsService, revealService, toaster, $location, $interval) {
        'use strict';

        var vm = this,
            participantsQueryIntervalPromise = undefined;

        vm.qrCodeText = "";

        var checkPreConditions = function () {
            if (typeof backlogService.getSelectedBacklog() === 'undefined') {
                toaster.pop('error', "", "Please select a backlog!", 10000);
                revealService.navigateToSlide("BacklogSlide");
                return false;
            }

            if (SurveyOptionsService.getOptions() == null || typeof SurveyOptionsService.getOptions() === 'undefined' || SurveyOptionsService.getOptions().length == 0) {
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
                Options: SurveyOptionsService.getOptions()
            }, function (data) {
                createQrCodeText();
                participantsQueryIntervalPromise = $interval(getParticipants, 3000);
            }, function () {
                toaster.pop('error', "", "Error creating the survey!", 10000);
            });
        };

        var createQrCodeText = function () {
            vm.qrCodeText = "http://" + $location.host() + ":" + $location.port() + "/surveys/" + vm.survey.Id;
        };

        var getParticipants = function () {
            if (typeof vm.survey !== 'undefined') {
                vm.participants = ParticipantsService.query({ surveyId: vm.survey.Id });
            }
        };

        $scope.$on("slidechanged:SurveyStartSlide", function (event, data) {
            if (checkPreConditions()) {
                createNewSurveyFromSelectedBacklog();
            }
        });

        $scope.$on("slidechanged", function (event, data) {
            if (typeof participantsQueryIntervalPromise !== 'undefined') {
                $interval.cancel(participantsQueryIntervalPromise);
            }
        });

        vm.startSurvey = function () {
            $interval.cancel(participantsQueryIntervalPromise);

            vm.survey.StartDate = new Date(); //Started
            SurveyService.start({ id: vm.survey.Id }, { StartDate: vm.survey.StartDate }, function () { }, function () {
                toaster.pop('error', "", "Error starting the survey!", 10000);
            });
        };
    });
})();