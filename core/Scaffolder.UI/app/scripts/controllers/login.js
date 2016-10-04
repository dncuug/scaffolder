'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:LoginCtrl
 * @description
 * # LoginCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('LoginCtrl', function($scope, $rootScope, $location, api) {

        $scope.incorrectCredential = false;

        $scope.auth = function() {

            api.signIn($scope.username, $scope.password).then(function(resposne) {

                if (resposne) {
                    $scope.incorrectCredential = false;
                    $location.path("/").search();
                    $rootScope.$emit("reload", true);
                } else {
                    $scope.incorrectCredential = true;
                }
            });
        };
    });