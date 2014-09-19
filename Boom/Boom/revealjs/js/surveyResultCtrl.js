(function () {
    angular.module('boom')

    .controller('SurveyResultCtrl', function ($scope, VotesService, SurveyOptionsService, toaster, revealService) {
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

                VotesService.query({ surveyId: survey.Id }, function (data) {
                    var votes = Enumerable.From(data).SelectMany(function (s) { return s.Options; }).ToArray();
                    var results = [];

                    votes.forEach(function (vote) {
                        if (Enumerable.From(results).Any(function (p) { return p.Id == vote.Option.Id }) == false) {
                            results.Add({ Id: vote.Option.Id, label: vote.Option.Description, count: vote.Weight });
                        } else {
                            var result = Enumerable.From(results).First(function (p) { return p.Id == vote.Option.Id });
                            result.Weight += vote.Weight;
                        }
                    });

                    vm.results = Enumerable.From(results).OrderByDescending(function (e) { return e.count; }).ToArray();

                });
            }
        });
    })
})();