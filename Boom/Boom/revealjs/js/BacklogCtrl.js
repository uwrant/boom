(function () {
    var app = angular.module('boom');

    app.controller("BacklogCtrl", function BacklogCtrl($scope, BacklogsService, backlogService) {
        'use strict';

        var vm = this;

        var selectFirstBacklog = function () {
            if (vm.backlogs.length != 0) {
                backlogService.setSelectedBacklog(vm.backlogs[0]);
            }
        };

        vm.newBacklog = { Name: "", Id: 0 };

        $scope.$on("slidechanged:BacklogSlide", function (event, data) {
            vm.backlogs = BacklogsService.query(function (data) {
                selectFirstBacklog();
            });
        });

        vm.addBacklog = function () {
            console.log('add backlog');
            var newBacklog = vm.newBacklog;

            BacklogsService.create(newBacklog, function (data) {
                vm.backlogs.push(data);
            });

            vm.newBacklog = { Name: "", Id: 0 };
        };

        vm.removeBacklog = function (backlog) {
            BacklogsService.delete({ id: backlog.Id }, function () {
                var index = vm.backlogs.indexOf(backlog);
                if (index > -1) {
                    vm.backlogs.splice(index, 1);
                }
            });
        };

        vm.selectBacklog = function (backlog) {
            backlogService.setSelectedBacklog(backlog);
        };

        vm.isBacklogSelected = function (backlog) {
            var selectedBacklog = backlogService.getSelectedBacklog();
            if (typeof selectedBacklog !== 'undefined') {
                return backlog.Id === selectedBacklog.Id;
            }

            return false;
        };
    });
})();