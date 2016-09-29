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

        $scope.editorForm = {};

        $scope.save = function() {
            var exist = !$routeParams.new;

            if (exist) {
                api.update($scope.table, $scope.record).then(function() {
                    var url = "/grid/" + $scope.table.name;
                    $location.path(url).search();
                });
            } else {
                api.insert($scope.table, $scope.record).then(function() {
                    var url = "/grid/" + $scope.table.name;
                    $location.path(url).search();
                });
            }
        }

        $scope.cancel = function() {
            var url = "/grid/" + $scope.table.name;
            $location.path(url).search('');
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
                    var record = !!response ? response.items[0] : null;
                    var dateTypeId = 20;

                    $scope.table.columns.forEach(function(c) {
                        if (c.type == dateTypeId) {
                            record[c.name] = new Date(record[c.name]);
                        }
                    })

                    $scope.record = record;

                });
            });

        }

        initializeEditor();
    });