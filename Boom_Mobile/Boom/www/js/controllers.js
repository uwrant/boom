angular.module('starter.controllers', [])

.controller('DashCtrl', function($scope) {
})

.controller('FriendsCtrl', function($scope, Friends) {
  $scope.friends = Friends.all();
})

.controller('FriendDetailCtrl', function($scope, $stateParams, Friends) {
  $scope.friend = Friends.get($stateParams.friendId);
})

.controller('AccountCtrl', function($scope) {
})

.controller('SurveysCtrl', function($scope, SurveyRest) {
        $scope.surveys = SurveyRest.allOpen();

})

.controller('SurveyDetailCtrl', function($scope, $stateParams, SurveyRest) {
    $scope.survey = SurveyRest.get($stateParams.surveyId);
});
