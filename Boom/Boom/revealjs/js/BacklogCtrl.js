(function () {
    var app = angular.module('boom');

    app.controller("BacklogCtrl", function BacklogCtrl($scope, Backlogs) {
        'use strict';

        var vm = this;

        vm.newBacklog = { Name: "" };

        $scope.$on("slidechanged:BacklogSlide", function (event, data) {
            vm.backlogs = Backlogs.query();
        });

        vm.addBacklog = function () {
            console.log('add backlog');
            var newBacklog = vm.newBacklog;

            Backlogs.create(newBacklog, function (data) {
                vm.backlogs.push(data);
            });

            vm.newBacklog = { Name: "" };
        };

        vm.removeBacklog = function (backlog) {
            Backlogs.remove(backlog, function () {
                var index = vm.backlogs.indexOf(backlog);
                if (index > -1) {
                    vm.backlogs.splice(index, 1);
                }
            });
        };
    });
})();