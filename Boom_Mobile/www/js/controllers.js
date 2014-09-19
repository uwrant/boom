angular.module('starter.controllers', [])

    .controller('SurveysCtrl', function ($scope, SurveyRest, $localstorage) {
        var participant = $localstorage.get('userName', 'USERNAME_NOT_SET');

        $scope.surveys = SurveyRest.allByParticipant({participant: participant});
    })

    .controller('SurveyDetailCtrl', function ($scope, $stateParams, $state, $localstorage, SurveyRest, VotesRest) {
        $scope.survey = SurveyRest.get({id: $stateParams.surveyId});

        $scope.vote = function () {
            var options = selectedOptions($scope.survey.Options);
            VotesRest.create({
                surveyId: $scope.survey.Id,
                Participant: {
                    Id: $localstorage.get('userId')
                },
                Options: options
            }).$promise.then(function () {
                    $scope.navigateToResults();
                })
        };

        var selectedOptions = function (options) {
            return Enumerable.From(options)
                .Where(function (option) {
                    return option.selected;
                })
                .ToArray();
        };

        $scope.hasOptionsSelected = function () {
            var optSelected = false;
            angular.forEach($scope.survey.Options, function (option) {
                if (option.selected) {
                    optSelected = true;
                }
            });
            return optSelected;
        };

        $scope.navigateToResults = function () {
            $state.go('tab.result-detail', {surveyId: $stateParams.surveyId});
        };
    });
