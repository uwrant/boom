angular.module('starter.services', ['jet.commons'])
    .factory('SurveyRest', function (smartResource) {
        return smartResource('/surveys/:id', {id: '@id'}, {
            allOpen: {
                method: 'GET',
                isArray: true,
                params: {open: true}
            },
            allByParticipant: {
                method: 'GET',
                isArray: true,
                params: {participant: '@id'}
            },
            all: {
                method: 'GET',
                isArray: true
            }
        });
    })
    .factory('ParticipantsRest', function (smartResource) {
        return smartResource('/surveys/:surveyId/participants', {surveyId: '@surveyId'}, {
            create: {
                method: 'POST'
            }
        })
    }
)

    .factory('VotesRest', function (smartResource) {
        return smartResource('/surveys/:surveyId/votes', {surveyId: '@surveyId'}, {
            create: {
                method: 'POST'
            }
        })
    })

    .factory('$localstorage', ['$window', function ($window) {
        return {
            set: function (key, value) {
                $window.localStorage[key] = value;
            },
            get: function (key, defaultValue) {
                return $window.localStorage[key] || defaultValue;
            },
            setObject: function (key, value) {
                $window.localStorage[key] = JSON.stringify(value);
            },
            getObject: function (key) {
                return JSON.parse($window.localStorage[key] || '{}');
            }
        }
    }]);
