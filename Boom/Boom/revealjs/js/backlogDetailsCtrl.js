'use strict';
(function () {
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, OptionsServiceMock, backlogService) {

        var vm = this;

        var OptionsService = OptionsServiceMock

        vm.options = OptionsService.query({backlogId: 1});

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