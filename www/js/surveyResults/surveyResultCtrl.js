angular.module('surveyResults')

.controller('SurveyResultCtrl', function($scope, $stateParams, SurveyResults) {
    $scope.friend = SurveyResults.get($stateParams.friendId);
    $scope.myData = [10,20,30,40,60];
})
