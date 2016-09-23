'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:DetailCtrl
 * @description
 * # DetailCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('DetailCtrl', function($scope, $routeParams, $location, api) {

        $scope.table = {};
        $scope.record = {};

        $scope.save = function() {
            var exist = false;

            if (exist) {
                api.update($scope.table, $scope.record).then(function() {
                    var url = "/grid/" + $scope.table.name;
                    $location.path(url);
                });
            } else {
                api.insert($scope.table, $scope.record).then(function() {
                    var url = "/grid/" + $scope.table.name;
                    $location.path(url);
                });
            }
        }

        $scope.cancel = function() {
            var url = "/grid/" + $scope.table.name;
            $location.path(url).search('');
        }

        function toArray(obj) {
            var keys = Object.keys(obj);

            var result = [];

            keys.forEach(function(value) {

            });
        }

        function initializeEditor() {

            var name = $routeParams.table;

            api.getTable(name).then(function(table) {
                $scope.table = table;
                $scope.title = table.title;

                if ($routeParams.new) {
                    $scope.record = {};
                    return;
                }

                var filter = {
                    TableName: table.name,
                    DetailMode: true,
                    Parameters: $routeParams
                };

                api.select(filter).then(function(response) {
                    $scope.record = !!response ? response[0] : null;
                });
            });

        }

        initializeEditor();

    });