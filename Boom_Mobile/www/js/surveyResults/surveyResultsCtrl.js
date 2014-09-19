angular.module('surveyResults')

.controller('SurveyResultsCtrl', function($scope, SurveyRest) {
    $scope.surveys = SurveyRest.all();
})
