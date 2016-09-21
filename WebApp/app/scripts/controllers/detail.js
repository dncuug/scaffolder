'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:DetailCtrl
 * @description
 * # DetailCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('DetailCtrl', function ($scope, $routeParams, $location, api) {

        $scope.table = {};
        $scope.record = {};

        $scope.test = function () {
            console.log($scope.record);
        }

        function initializeEditor() {

            var name = $routeParams.table;
            var id = $routeParams.id;

            api.getTable(name).then(function (table) {
                $scope.table = table;
                $scope.title = table.title;
            });

        }

        initializeEditor();

    });