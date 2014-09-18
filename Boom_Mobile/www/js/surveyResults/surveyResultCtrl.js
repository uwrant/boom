angular.module('starter.controllers')

.controller('SurveyResultCtrl', function($scope, $stateParams, SurveyResults) {
    $scope.friend = SurveyResults.get($stateParams.friendId);
})
