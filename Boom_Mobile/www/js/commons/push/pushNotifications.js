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

      var platformType = platformTypes.unknown;
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
        } else if (platformType === platformTypes.ios) {
          initAPN();
        }
      }

      function subscribe(tag) {
        switch (platformType) {
          case platformTypes.android:
            subscribeAndroid(tag);
            break;
          case platformTypes.ios:
            subscribeIOS(tag);
            break;
          default:
            console.log("Push notifications not supported.");
        }
      }

      function subscribeAndroid(tag){
        if (!app.gcmId) {
          alert("Device registration token unknown.");
          return;
        }

        // Template registration.
        var template = "{ \"data\" : {\"message\":\"$(message)\"}}";

        hub.gcm.register(app.gcmId, [tag], "myTemplate", template).done(function () {
          console.log("Registered with hub!");
        }).fail(function (error) {
          alert("Failed registering with hub: " + error);
        });
      }

      function subscribeIOS(tag){
        if (!app.apnToken) {
          alert("Device registration token unknown.");
          return;
        }

        // Template registration.
        var template = "{ \"data\" : {\"message\":\"$(message)\"}}";

        hub.apns.register(app.apnToken, [tag], "myTemplate", template).done(function () {
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

      function initAPN(){
        var iosConfig = {
          "badge": "true",
          "ecb": "app.onNotificationAPN"
        }

        $cordovaPush.register(iosConfig).then(function (result) {
          console.log("$cordovaPush.register - device token: " + result);
          app.apnToken = result;
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
          console.log("gcmid: " + e.regid);
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

  app.onNotificationAPN = function (event){
    if (event.message){
      // Display the alert message in an alert.
      alert(event);
    }

    // // Other possible notification stuff we don't use in this sample.
    // if (event.sound){
    // var snd = new Media(event.sound);
    // snd.play();
    // }

    // if (event.badge){

    // pushNotification.setApplicationIconBadgeNumber(successHandler, errorHandler, event.badge);
    // }
  }
})();

