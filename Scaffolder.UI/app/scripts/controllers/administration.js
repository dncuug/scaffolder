'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:AdministrationCtrl
 * @description
 * # AdministrationCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('AdministrationCtrl', function ($scope, api) {

        $scope.progress = false;

        $scope.rebuildScheme = function () {

            $scope.progress = true;

            api.rebuildScheme().then(function () {

                $scope.progress = false;
                $scope.status = 'Database schema updated successfully';
            });
        }

        $scope.refreshPage = function () {
            location.reload();
        }
    });