'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:AdministrationCtrl
 * @description
 * # AdministrationCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('AdministrationCtrl', function($scope, api) {
        $scope.initizlizeDatrabaseScheme = function() {
            api.initizlizeDatrabaseScheme().then(function() {
                $scope.status = 'Done';
            });
        }
    });