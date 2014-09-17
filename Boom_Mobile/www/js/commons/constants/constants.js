(function() {
    'use strict';
    var AppConstants = angular.module('jet.commons.constants');

    // choose one of the following:
    // -Mac
    // -Windows
    // -Azure
    var backend = 'Windows';

    switch(backend) {
        case 'Mac':
            // ----- Mac Backend Config
            AppConstants.constant('REST_API_URL', 'http://localhost:5004');
            break;
        case 'Windows':
            // ----- .NET Backend Config
            AppConstants.constant('REST_API_URL', 'http://localhost:5001');
            break;
        case 'Azure':
            // ----- Azures Backend Config
            AppConstants.constant('REST_API_URL', 'http://boom-test.azurewebsites.net/');
            break;
    }
})();

