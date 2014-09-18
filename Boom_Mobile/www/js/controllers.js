angular.module('starter.controllers', [])

.controller('SurveysCtrl', function($scope, SurveyRest, pushNotifications, PUSH_NOTIFICATION_EVENT) {
    $scope.surveys = SurveyRest.allOpen();

    $scope.testPush = function (){
      pushNotifications.subscribe("testTag");
    }

    $scope.$on(PUSH_NOTIFICATION_EVENT, function(event, message){
      $scope.$apply(function(){
        $scope.pushMessage = message;
      });
    });
})

.controller('SurveyDetailCtrl', function($scope, $stateParams, SurveyRest, ParticipantsRest) {
        $scope.survey = SurveyRest.get({ id: $stateParams.surveyId });

        $scope.participant = {Id: undefined};
        $scope.participate = function (participant) {
            $scope.participant = participant;
            ParticipantsRest.create({
                surveyId: $scope.survey.Id,
                participant: $scope.participant
            });
        };

        $scope.hasJoined = function(){
            return $scope.participant.Id !== undefined;
        };

        $scope.hasOptionsSelected = function(){
            var optSelected = false;
            angular.forEach($scope.survey.Options, function(option){
                if(option.selected){
                    optSelected = true;
                }
            });
            return optSelected;
        };
});
