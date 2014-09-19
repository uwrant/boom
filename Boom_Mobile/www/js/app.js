(function(window, angular){
    'use strict';

    // Ionic Starter App

    // angular.module is a global place for creating, registering and retrieving Angular modules
    // 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
    // the 2nd parameter is an array of 'requires'
    // 'starter.services' is found in services.js
    // 'starter.controllers' is found in controllers.js
    angular.module('starter', ['ionic', 'starter.controllers', 'starter.services', 'surveyResults', 'scanSurvey'])

        .run(function($ionicPlatform, pushNotifications) {
            $ionicPlatform.ready(function() {
                // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
                // for form inputs)
                if(window.cordova && window.cordova.plugins.Keyboard) {
                    window.cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
                }
                if(window.StatusBar) {
                    // org.apache.cordova.statusbar required
                    window.StatusBar.styleDefault();
                }

                pushNotifications.init();
            });
        })

        .config(function($stateProvider, $urlRouterProvider) {

            // Ionic uses AngularUI Router which uses the concept of states
            // Learn more here: https://github.com/angular-ui/ui-router
            // Set up the various states which the app can be in.
            // Each state's controller can be found in controllers.js
            $stateProvider

                // setup an abstract state for the tabs directive
                .state('tab', {
                    url: '/tab',
                    abstract: true,
                    templateUrl: 'templates/tabs.html'
                })

                // Each tab has its own nav history stack
                .state('tab.surveys', {
                    url: '/surveys',
                    views: {
                        'tab-surveys': {
                            templateUrl: 'templates/tab-surveys.html',
                            controller: 'SurveysCtrl'
                        }
                    }
                })

                // Scanning barcode
                .state('tab.scanSurvey', {
                    url: '/scanSurvey',
                    views: {
                        'tab-scan': {
                            templateUrl: 'js/scanSurvey/scanSurvey.html',
                            controller: 'ScanSurveyCtrl as scan'
                        }
                    }
                })
                .state('tab.survey-detail', {
                    url: '/survey/:surveyId',
                    views: {
                        'tab-surveys': {
                            templateUrl: 'templates/survey-detail.html',
                            controller: 'SurveyDetailCtrl'
                        }
                    }})

                .state('tab.results', {
                    url: '/results',
                    views: {
                        'tab-results': {
                            templateUrl: 'js/surveyResults/surveyResults.html',
                            controller: 'SurveyResultsCtrl'
                        }
                    }
                })
                .state('tab.result-detail', {
                    url: '/results/:surveyId',
                    views: {
                        'tab-results': {
                            templateUrl: 'js/surveyResults/surveyResult.html',
                            controller: 'SurveyResultCtrl'
                        }
                    }
                });

            // if none of the above states are matched, use this as the fallback
            $urlRouterProvider.otherwise('/tab/surveys');
        });
})(window, angular);
