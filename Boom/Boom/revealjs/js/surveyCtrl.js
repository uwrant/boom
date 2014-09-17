(function () {
    var app = angular.module('boom');

    app.controller("SurveyCtrl", function BacklogCtrl($scope, SurveyService, backlogService) {
        'use strict';

        var vm = this;
        vm.qrCodeText = "''";

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
            createNewSurveyFromSelectedBacklog();
        });

        vm.startSurvey = function () {
            vm.survey.State = 1; //Started
            vm.survey.$save();
        };
    });
})();