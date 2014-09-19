(function(angular){
    angular.module('surveyResults')

        /**
         * A simple example service that returns some data.
         */
        .factory('SurveyResults', function() {
            // Might use a resource here that returns a JSON array

            // Some fake testing data
            var surveyResults = [
                { id: 0, name: 'Social Events 2014 (January)' },
                { id: 1, name: 'Social Events 2014 (March)' },
                { id: 2, name: 'Team Meeting Presentation July 2014' },
                { id: 3, name: 'Team Meeting Presentation August 2014' }
            ];

            return {
                all: function() {
                    return surveyResults;
                },
                get: function(surveyId) {
                    // Simple index lookup
                    return surveyResults[surveyId];
                }
            };
        });
})(angular);

