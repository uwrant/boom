angular.module('scanSurvey')

  .controller('ScanSurveyCtrl', function ($cordovaBarcodeScanner, $state) {
    var vm = this;

    vm.title = "Scan a QR Code for adding a survey";

    vm.scanBarcode = function () {
      $cordovaBarcodeScanner.scan().then(function (imageData) {
        // Success! Barcode data is here
        var backendUrl = imageData.text;
        var idStartIndex = backendUrl.lastIndexOf("/") + 1;
        var id = backendUrl.substr(idStartIndex);
        $state.go('tab.survey-detail', {surveyId: id});
      }, function (err) {
        // An error occured. Show a message to the user

      });
    };

    vm.scanBarcode();

    return vm;
  });
