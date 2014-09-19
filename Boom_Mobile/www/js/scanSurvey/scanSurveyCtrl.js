angular.module('scanSurvey')

    .controller('ScanSurveyCtrl', function ($cordovaBarcodeScanner, $state, $ionicPopup, SurveyRest, ParticipantsRest, pushNotifications) {
        var vm = this;

        vm.title = "Scan a QR Code for adding a survey";

        vm.scanBarcode = function () {
            $cordovaBarcodeScanner.scan().then(function (imageData) {
                // Success! Barcode data is here
                var backendUrl = imageData.text;
                var idStartIndex = backendUrl.lastIndexOf("/")

                if (idStartIndex == -1) {
                    $ionicPopup.alert({
                            title: 'Unable to parse the QR code',
                            template: 'The encoded URL is not valid.' + imageData.text
                        }
                    );

                    return;
                }

                var id = backendUrl.substr(idStartIndex + 1);

                SurveyRest.get({surveyId: id})
                    .$promise.then(function (survey) {
                        participate(id);
                        $state.go('tab.survey-detail', {surveyId: id});
                    }, function (error) {
                        $ionicPopup.alert({
                                title: 'No survey found',
                                template: 'No survey exists for the given ID: ' + imageData.text + '(' + error + ')'
                            }
                        );
                    });
                ;
            }, function (err) {
                // An error occured. Show a message to the user
                $ionicPopup.alert({
                    title: 'Unable to scan the QR code',
                    template: 'Too bad, something went wrong.'
                });
            });
        };

        function participate(surveyId) {

            var participant = $localstorage.get('userName', 'your name here');
            ParticipantsRest.create({
                surveyId: surveyId,
                Name: participant
            }).$promise.then(function (participant) {
                    $localstorage.set('userId', participant.Id)
                    pushNotifications.subscribe(surveyId);
                });
        };

        vm.scanBarcode();

        return vm;
    });
