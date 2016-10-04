'use strict';

/**
 * @ngdoc service
 * @name webAppApp.authProvider
 * @description
 * # authProvider
 * Service in the webAppApp.
 */
angular.module('webAppApp')
  .service('authProvider', function (api) {
    // AngularJS will instantiate a singleton by calling "new" on this function
    return {
      isLoggedIn: function () {
        return !!api.getToken()
      }
    };

  });


