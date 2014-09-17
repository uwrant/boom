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
                Name: 'FirstSurvey',
                MaxNumberOfOptions: 3,
                Options: [
                    {
                        Id: 1,
                        Name: "Paintball"
                    },
                    {
                        Id: 2,
                        Name: "Kart"
                    },
                    {
                        Id: 3,
                        Name: "Counter Strike Evening"
                    },
                    {
                        Id: 4,
                        Name: "Climbing"
                    },
                    {
                        Id: 5,
                        Name: "Blaming friends"
                    }
                ]
            },
            {
                Id: 2,
                Name: 'SecondSurvey',
                MaxNumberOfOptions: 1,
                Options: [
                    {
                        Id: 1,
                        Name: "Cooking"
                    },
                    {
                        Id: 2,
                        Name: "Coding"
                    },
                    {
                        Id: 3,
                        Name: "Cinema"
                    }
                ]
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
