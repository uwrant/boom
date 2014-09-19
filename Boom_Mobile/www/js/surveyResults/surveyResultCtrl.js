angular.module('surveyResults')

.controller('SurveyResultCtrl', function($scope, $stateParams, SurveyResults) {
    $scope.friend = SurveyResults.get($stateParams.friendId);
    $scope.myData = [
        { label: 'Kart fahren', count: 12 },
        { label: 'Klettern', count: 4 },
        { label: 'Eis essen', count: 7 },
        { label: 'Grillen', count: 2 },
        { label: 'Lasertag', count: 19 }];
})
