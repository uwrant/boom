angular.module('starter.controllers')

.controller('SurveyResultCtrl', function($scope, $stateParams, Friends) {
    $scope.friend = Friends.get($stateParams.friendId);
})
