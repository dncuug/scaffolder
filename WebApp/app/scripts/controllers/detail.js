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


        function initializeEditor() {

            var name = $routeParams.table;
            var id = $routeParams.id;

            api.getTable(name).then(function(table) {
                $scope.title = table.title;
            });

        }

        initializeEditor();

    });