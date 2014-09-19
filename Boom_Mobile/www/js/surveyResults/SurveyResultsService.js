(function(angular){
    angular.module('surveyResults')
        .factory('VotesRest', function(smartResource) {
            return smartResource('/surveys/:id/result', {}, {
            });
        });
})(angular);

