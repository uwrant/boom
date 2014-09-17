angular.module('starter.controllers')

.controller('SurveyResultsCtrl', function($scope, Friends) {
    $scope.friends = Friends.all();
})
