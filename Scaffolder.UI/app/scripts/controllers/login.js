'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('LoginCtrl', function ($scope, $location, api) {

        $scope.auth = function () {

            api.signIn($scope.username, $scope.password).then(function (resposne) {
              debugger;
                if (resposne) {
                  $location.path("/");
                }
            });
        };
    });
