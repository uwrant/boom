angular.module('settings')

    .controller('SettingsCtrl', function ($state, $localstorage) {
        var vm = this;

        vm.title = "Adjust your username";

        vm.userName = $localstorage.get('userName', 'your name here');

        vm.save = function(){
            $localstorage.set('userName', vm.userName);
            $state.go('tab.surveys');
        }

        return vm;
    });
