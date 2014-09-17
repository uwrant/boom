(function () {
    var app = angular.module('boom');

    app.controller("BacklogDetailCtrl", function BacklogDetailCtrl($scope, OptionsService) {
        'use strict';

        var vm = this;

        vm.options = OptionsService.query();

        vm.newOption = { Name: '', disabled: false };
        vm.disabledFilter = { disabled: true };

        vm.addOptions = function () {
            var newOption = vm.newOption;
            if (!newOption.Name.length) {
                return;
            }

            vm.options.push(newOption);

            vm.newOption = { speaker: "", title: "", disabled: false };
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