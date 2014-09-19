angular.module('starter.services', ['jet.commons'])
.factory('SurveyRest', function(smartResource) {
        return smartResource('/surveys/:surveyId', { surveyId:'@id' }, {
            get: {
                method: 'GET'
            },
            allOpen: {
                method: 'GET',
                isArray: true,
                params: { open: true }
            },
            all: {
                method: 'GET',
                isArray: true
            }
        });
    })
.factory('ParticipantsRest', function(smartResource) {
        return smartResource('/surveys/:surveyId/participants', { surveyId:'@surveyId' }, {
            create:{
                method: 'POST'
            }
        })
    }
)

.factory('VotesRest', function(smartResource) {
    return smartResource('/surveys/:surveyId/votes', { surveyId:'@surveyId' }, {
        create:{
            method: 'POST'
        }
    })
}
);
