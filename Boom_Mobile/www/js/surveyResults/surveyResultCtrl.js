angular.module('surveyResults')

.controller('SurveyResultCtrl', function($scope, $stateParams, SurveyRest, VotesRest) {
        $scope.survey = SurveyRest.get({ id: $stateParams.surveyId });
        $scope.myData = [ { label: 'dummy', count: 1 } ];

        VotesRest.query({ id: $stateParams.surveyId }).$promise.then(
            function(result){
                var votes = result;

                var myData = Enumerable.From(votes)
                    .Select(function(vote) { return { label: vote.Description, count: vote.Count }; })
                    .OrderByDescending()
                    .ToArray();

                $scope.myData = myData;
        });
})
