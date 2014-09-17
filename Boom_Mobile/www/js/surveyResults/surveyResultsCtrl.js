angular.module('starter.controllers')

.controller('SurveyResultsCtrl', function($scope, SurveyResults) {
    $scope.friends = SurveyResults.all();
})
