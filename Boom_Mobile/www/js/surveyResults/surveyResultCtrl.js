angular.module('surveyResults')

.controller('SurveyResultCtrl', function($scope, $stateParams, SurveyRest, SurveyResults) {
    $scope.survey = SurveyRest.get({ id: $stateParams.surveyId });
    $scope.myData = [
        { label: 'Kart fahren', count: 12 },
        { label: 'Klettern', count: 4 },
        { label: 'Eis essen', count: 7 },
        { label: 'Grillen', count: 2 },
        { label: 'Lasertag', count: 19 }];
})
