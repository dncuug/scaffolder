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

            debugger;
            api.auth($scope.username, $scope.password).then(function (resposne) {
                if (resposne) {
                    $location.redirect('/')
                }
            });
        };
    });