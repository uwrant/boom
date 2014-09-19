(function () {
    angular.module('boom')

    .controller('SurveyResultCtrl', function ($scope, VotesService, SurveyOptionsService) {
        var vm = this;

        var survey = SurveyOptionsService.getCurrentSurvey();

        VotesService.query({ surveyId: survey.Id }, function (data) {
            var blub = [];

            data.forEach(function (vote) {

            });

        });

        vm.results = Enumerable.From([
            { label: 'Kart fahren', count: 12 },
            { label: 'Klettern', count: 4 },
            { label: 'Eis essen', count: 7 },
            { label: 'Grillen', count: 2 },
            { label: 'Lasertag', count: 19 }]).OrderByDescending(function (e) { return e.count; }).ToArray();
    })
})();