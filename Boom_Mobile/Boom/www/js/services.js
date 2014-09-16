angular.module('starter.services', [])

/**
 * A simple example service that returns some data.
 */
.factory('Friends', function() {
  // Might use a resource here that returns a JSON array

  // Some fake testing data
  var friends = [
    { id: 0, name: 'Scruff McGruff' },
    { id: 1, name: 'G.I. Joe' },
    { id: 2, name: 'Miss Frizzle' },
    { id: 3, name: 'Ash Ketchum' }
  ];

  return {
    all: function() {
      return friends;
    },
    get: function(friendId) {
      // Simple index lookup
      return friends[friendId];
    }
  }
})
.factory('SurveyRest', function() {
        var surveys = [
            {
                Id: 1,
                Name: 'FirstSurvey'
            },
            {
                Id: 2,
                Name: 'SecondSurvey'
            }
        ];

        return {
            allOpen: allOpen,
            get: get
        };

        function allOpen() {
            return surveys;
        }

        function get(id) {
            var index;
            for(index in surveys) {
                var survey = surveys[index];
                if(survey.Id == id) {
                    return survey;
                }
            }

            throw "Survey not found!";
        }
    });
