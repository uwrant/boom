var app = app || {};

(function () {
  'use strict';

  var rootScope;
  var notificationEventName;

  angular.module('jet.commons.push')
    .factory('pushNotifications', function ($window, $cordovaPush, $rootScope, AZURE_API_URL, AZURE_API_KEY, GOOGLE_SENDER_ID, PUSH_NOTIFICATION_EVENT) {
      rootScope = $rootScope;
      notificationEventName = PUSH_NOTIFICATION_EVENT;

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

        mobileClient = new WindowsAzure.MobileServiceClient(AZURE_API_URL, AZURE_API_KEY);
        hub = new NotificationHub(mobileClient);

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
          alert("Registered with hub!");
        }).fail(function (error) {
          alert("Failed registering with hub: " + error);
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

