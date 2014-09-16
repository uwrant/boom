/**
 * Created by mous on 16/09/14.
 */
angular.module('starter').config(function($stateProvider, $urlRouteProvider){
    $stateProvider.state('home', {
        url: '/',
        templateUrl: 'js/surveys/surveys.html',
        controller: 'SurveysCtrl'
    });

    $urlRouteProvider.otherwise('/');
});