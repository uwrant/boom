angular.module('scanSurvey')

    .controller('ScanSurveyCtrl', function($cordovaBarcodeScanner, $state) {
        var vm = this;

        vm.title = "Scan a QR Code for adding a survey";

        vm.scanBarcode = function() {
            $cordovaBarcodeScanner.scan().then(function(imageData) {
                // Success! Barcode data is here
                $state.go('tab.survey-detail',{surveyId: imageData});
            }, function(err) {
                // An error occured. Show a message to the user
                
            });
        };

        return vm;
    });
