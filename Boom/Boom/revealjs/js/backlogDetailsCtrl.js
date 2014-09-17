(function () {
    'use strict';
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, OptionsServiceMock, backlogService, surveyService) {

        var vm = this;

        var OptionsService = OptionsServiceMock

        $scope.$on("slidechanged:BacklogContentSlide", function () {
            // TODO: check preconditions
            var selectedBacklog = backlogService.getSelectedBacklog();
            vm.options = OptionsService.query({ backlogId: selectedBacklog.Id });
        });

        $scope.$watch("ctrl.options", function () {
            var blub = undefined;

            surveyService.setOptions(Enumerable.From(vm.options).Where(function (p) { return p.disabled == undefined || p.disabled == false }).ToArray());
        }, true);

        vm.newOption = { Name: '', disabled: false };
        vm.disabledFilter = { disabled: true };

        vm.addOption = function () {
            var newOption = vm.newOption;

            if (vm.isNewOptionValid == false) {
                return;
            }

            vm.options.push(newOption);

            vm.newOption = { Name: "", disabled: false };
        }

        vm.isNewOptionValid = function () {
            return vm.newOption.Name.length;
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