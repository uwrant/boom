(function () {
    'use strict';
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, OptionsService, backlogService, revealService, SurveyOptionsService, toaster) {

        var vm = this;
        vm.options = {};

        var checkPreConditions = function () {
            if (typeof backlogService.getSelectedBacklog() === 'undefined') {
                toaster.pop('error', "", "Please select a backlog!", 10000);
                revealService.navigateToSlide("BacklogSlide");
                return false;
            }

            return true;
        }

        $scope.$on("slidechanged:BacklogContentSlide", function () {
            if (checkPreConditions()) {
                var selectedBacklog = backlogService.getSelectedBacklog();
                vm.options = OptionsService.query({ backlogId: selectedBacklog.Id });
            }
        });

        $scope.$watch("ctrl.options", function () {
            SurveyOptionsService.setOptions(Enumerable.From(vm.options).Where(function (p) { return p.disabled == undefined || p.disabled == false }).ToArray());
        }, true);

        vm.newOption = { Description: '', disabled: false };
        vm.disabledFilter = { disabled: true };

        vm.addOption = function () {
            var backlogId = backlogService.getSelectedBacklog().Id;
            var newOption = vm.newOption;

            if (vm.isNewOptionValid == false) {
                return;
            }

            OptionsService.create({ backlogId: backlogId }, { Description: newOption.Description }, function (data) {
                    vm.options.push(data);
                }, function () {
                    toaster.pop('error', '', 'Error creating the entry!');
                });

            vm.newOption = { Description: "", disabled: false };
        }

        vm.isNewOptionValid = function () {
            return vm.newOption.Description.length;
        }

        vm.removeOption = function (option) {
            var index = vm.options.indexOf(option);
            if (index > -1) {
                vm.options.splice(index, 1);
            }
        }

        vm.notNowOption = function (option) {
            option.disabled = true;
        }

        vm.nowOption = function (option) {
            option.disabled = false;
        }
    });
})();