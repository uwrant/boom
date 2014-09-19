angular.module('scanSurvey')

    .controller('ScanSurveyCtrl', function($cordovaBarcodeScanner) {
        var vm = this;

        this.title = "Scan a QR Code for adding a survey";

        vm.scanBarcode = function() {
            $cordovaBarcodeScanner.scan().then(function(imageData) {
                // Success! Barcode data is here

            }, function(err) {
                // An error occured. Show a message to the user

            });
        };

        // NOTE: encoding not functioning yet
        vm.encodeData = function() {
            $cordovaBarcodeScanner.encode(BarcodeScanner.Encode.TEXT_TYPE, "http://www.nytimes.com").then(function(success) {
                // Success!
            }, function(err) {
                // An error occured. Show a message to the user

            });
        }

        return vm;
    });
