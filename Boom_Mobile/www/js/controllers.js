angular.module('starter.controllers', [])

.controller('SurveysCtrl', function($scope, SurveyRest) {
    $scope.surveys = SurveyRest.allOpen();
})

.controller('SurveyDetailCtrl', function($scope, $stateParams, $state, SurveyRest, ParticipantsRest, pushNotifications, PUSH_NOTIFICATION_EVENT) {
        $scope.survey = SurveyRest.get({ id: $stateParams.surveyId });

        $scope.participant = {Id: undefined};
        $scope.participate = function (participant) {

            $scope.participant = participant;
            ParticipantsRest.create({
                surveyId: $scope.survey.Id,
                Name: $scope.participant.Name
            }).$promise.then(function(participant) {
                    $scope.participant = participant;
                    pushNotifications.subscribe($scope.survey.Id);
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

        $scope.navigateToResults = function() {
            $state.go('tab.result-detail', { surveyId: $stateParams.surveyId });
        };
});
