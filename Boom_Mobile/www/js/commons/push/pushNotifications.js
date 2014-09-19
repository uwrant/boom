var app = app || {};

(function () {
  'use strict';

  var rootScope;
  var notificationEventName;

  var platformTypes = {
    unknown: 0,
    android: 1,
    ios: 2
  };

  angular.module('jet.commons.push')
    .factory('pushNotifications', function ($window, $cordovaPush, $rootScope, AZURE_API_URL, AZURE_API_KEY, GOOGLE_SENDER_ID, PUSH_NOTIFICATION_EVENT, $cordovaDevice) {
      rootScope = $rootScope;
      notificationEventName = PUSH_NOTIFICATION_EVENT;

      var platformType;
      var mobileClient;
      var hub;

      return {
        init: init,
        subscribe: subscribe
      };

      function init() {
        if (!$window.cordova) {
          return;
        }

        platformType = detectPlatformType();

        mobileClient = new WindowsAzure.MobileServiceClient(AZURE_API_URL, AZURE_API_KEY);
        hub = new NotificationHub(mobileClient);

        if (platformType === platformTypes.android){
          initGCMPush();
        }
      }

      function subscribe(tag) {
        if (!app.gcmId) {
          alert("Device registration token unknown.");
          return;
        }

        if (!hub) {
          alert("Push notifications not initialized.");
        }

        // Template registration.
        var template = "{ \"data\" : {\"message\":\"$(message)\"}}";

        hub.gcm.register(app.gcmId, [tag], "myTemplate", template).done(function () {
          console.log("Registered with hub!");
        }).fail(function (error) {
          alert("Failed registering with hub: " + error);
        });
      }

      function detectPlatformType(){
        var device = $cordovaDevice.getDevice();
        if (device.platform == 'android' || device.platform == 'Android' || device.platform == "amazon-fireos"){
          return platformTypes.android;
        }

        if (device.platform === 'iOS'){
          return platformTypes.ios;
        }

        return platformTypes.unknown;
      }

      function initGCMPush(){
        var androidConfig = {
          "senderID": GOOGLE_SENDER_ID,
          "ecb": "app.onNotificationGCM"
        };

        $cordovaPush.register(androidConfig).then(function (result) {
          console.log("$cordovaPush.register: " + result);
        }, function (err) {
          alert(err);
        });
      }
    });

  app.onNotificationGCM = function (e) {
    switch (e.event) {
      case 'registered':
        // Handle the registration.
        if (e.regid.length > 0) {
          alert("gcm id " + e.regid);
          app.gcmId = e.regid;
        }
        break;

      case 'message':

        // Handle the received notification when the app is running
        // and display the alert message.
        alert(e.payload.message);
        rootScope.$broadcast(notificationEventName, e.payload.message);
        break;

      case 'error':
        alert('GCM error: ' + e.message);
        break;

      default:
        alert('An unknown GCM event has occurred');
        break;
    }
  };
})();

