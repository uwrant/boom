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
        SurveyRest.allOpen($scope.surveys, function success(response){
            $scope.surveys = response;
        }, function error(error){
        });
})

.controller('SurveyDetailCtrl', function($scope, $stateParams, SurveyRest) {
    // $scope.survey = SurveyRest.get($stateParams.surveyId);
});
