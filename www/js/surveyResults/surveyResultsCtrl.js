angular.module('surveyResults')

.controller('SurveyResultsCtrl', function($scope, SurveyResults) {
    $scope.friends = SurveyResults.all();
})
