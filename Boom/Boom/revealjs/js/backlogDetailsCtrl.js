(function () {
    'use strict';
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, OptionsService, backlogService, surveyService, toaster) {

        var vm = this;
        vm.options = {}; 

        $scope.$on("slidechanged:BacklogContentSlide", function () {
            // TODO: check preconditions
            var selectedBacklog = backlogService.getSelectedBacklog();
            vm.options = OptionsService.query({ backlogId: selectedBacklog.Id });

            // TODO ozu: check if digest call is necessarry when calling the real service
            $scope.$digest();
        });

        $scope.$watch("ctrl.options", function () {
            surveyService.setOptions(Enumerable.From(vm.options).Where(function (p) { return p.disabled == undefined || p.disabled == false }).ToArray());
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

        vm.toggleOption = function (option) {
            option.disabled = !option.disabled;
        }
    });
})();