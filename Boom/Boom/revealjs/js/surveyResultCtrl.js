(function () {
    angular.module('boom')

    .controller('SurveyResultCtrl', function ($scope, VotesService, SurveyOptionsService, toaster, revealService) {
        var vm = this;

        var checkPreConditions = function () {
            var survey = SurveyOptionsService.getCurrentSurvey();
            if (typeof survey === 'undefined') {
                toaster.pop('error', "", "Please create a survey!", 10000);
                revealService.navigateToSlide("SurveyStartSlide");
                return false;
            }
            if (survey.EndDate === null) {
                toaster.pop('error', "", "Please stop the survey!", 10000);
                revealService.navigateToSlide("SurveyProgressSlide");
                return false;
            }
            return true;
        }

        $scope.$on("slidechanged:SurveyResultSlide", function (event, data) {
            if (checkPreConditions()) {
                var survey = SurveyOptionsService.getCurrentSurvey();

                VotesService.query({ surveyId: survey.Id }, function (data) {
                    var blub = [];

                    data.forEach(function (vote) {

                    });

                });
            }
        });

        vm.results = Enumerable.From([
            { label: 'Kart fahren', count: 12 },
            { label: 'Klettern', count: 4 },
            { label: 'Eis essen', count: 7 },
            { label: 'Grillen', count: 2 },
            { label: 'Lasertag', count: 19 }]).OrderByDescending(function (e) { return e.count; }).ToArray();
    })
})();