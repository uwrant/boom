(function () {
    angular.module('boom')

    .controller('SurveyResultCtrl', function ($scope, ResultsService, SurveyOptionsService, toaster, revealService) {
        var vm = this;

        vm.results = Enumerable.From([
            { label: 'Kart fahren', count: 12 },
            { label: 'Klettern', count: 4 },
            { label: 'Eis essen', count: 7 },
            { label: 'Grillen', count: 2 },
            { label: 'Lasertag', count: 19 }]).OrderByDescending(function (e) { return e.count; }).ToArray();


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

                ResultsService.query({ surveyId: survey.Id }, function (data) {
                    vm.results = Enumerable.From(data).OrderByDescending(function (o) { return o.Count; }).Select(function (e) { return { label: e.Description, count: e.Count }; }).ToArray();
                });
            }
        });
    })
})();