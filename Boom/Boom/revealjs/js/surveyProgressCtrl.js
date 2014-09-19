(function () {
    var app = angular.module('boom');

    app.controller("SurveyProgressCtrl", function SurveyProgressCtrl($scope, SurveyService, SurveyOptionsService, VotesService, revealService, toaster, $interval) {
        'use strict';

        var vm = this,
            votesQueryIntervalPromise = undefined;

        vm.votes = [];

        var checkPreConditions = function () {
            var survey = SurveyOptionsService.getCurrentSurvey();
            if (typeof survey === 'undefined') {
                toaster.pop('error', "", "Please create a survey!", 10000);
                revealService.navigateToSlide("SurveyStartSlide");
                return false;
            }
            if (survey.StartDate === null) {
                toaster.pop('error', "", "Please start the survey!", 10000);
                revealService.navigateToSlide("SurveyStartSlide");
                return false;
            }
            return true;
        }

        var getVotes = function () {
            if (typeof vm.survey !== 'undefined') {
                vm.votes = VotesService.query({ surveyId: vm.survey.Id });
            }
        };

        $scope.$on("slidechanged:SurveyProgressSlide", function (event, data) {
            if (checkPreConditions()) {
                vm.survey = SurveyOptionsService.getCurrentSurvey();
                votesQueryIntervalPromise = $interval(getVotes, 3000);
            }
        });

        $scope.$on("slidechanged", function (event, data) {
            if (typeof votesQueryIntervalPromise !== 'undefined') {
                $interval.cancel(votesQueryIntervalPromise);
            }
        });

        vm.stopSurvey = function () {
            $interval.cancel(votesQueryIntervalPromise);

            vm.survey.EndDate = new Date(); //Ended
            SurveyService.patch({ id: vm.survey.Id }, { EndDate: vm.survey.EndDate }, function () { }, function () {
                toaster.pop('error', "", "Error stopping the survey!", 10000);
            });
        };
    });
})();