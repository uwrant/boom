(function () {
    var app = angular.module('boom');

    app.controller("BacklogCtrl", function BacklogCtrl($scope, Backlogs, backlogService) {
        'use strict';

        var vm = this;

        var selectFirstBacklog = function () {
            if (vm.backlogs.length != 0) {
                backlogService.setSelectedBacklog(vm.backlogs[0]);
            }
        };

        vm.newBacklog = { Name: "" };

        $scope.$on("slidechanged:BacklogSlide", function (event, data) {
            vm.backlogs = Backlogs.query(function (data) {
                selectFirstBacklog();
            });
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
            Backlogs.delete({ id: backlog.Id },  function() {
                var index = vm.backlogs.indexOf(backlog);
                if (index > -1) {
                    vm.backlogs.splice(index, 1);
                }
            });
        };

        vm.selectBacklog = function (backlog) {
            backlogService.setSelectedBacklog(backlog);

            Reveal.next();
        };
    });
})();