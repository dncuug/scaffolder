'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:GridCtrl
 * @description
 * # GridCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('GridCtrl', function($scope, $routeParams, api) {

        $scope.gridOptions = {
            enableSorting: true,
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25,
            columnDefs: [],
            data: [{
                "first-name": "Cox",
                "friends": ["friend0"],
                "address": { street: "301 Dove Ave", city: "Laurel", zip: "39565" },
                "getZip": function() { return this.address.zip; }
            }]
        };

        $scope.title = '';

        $scope.clickHandler = {
            delete: function(value) {
                alert('Name: ' + value);
            },
            edit: function(value) {
                alert('Name: ' + value);
            }
        };

        function initializeGrid() {

            var table = $routeParams.table;

            api.getTable(table).then(function(response) {

                $scope.title = response.title;
                $scope.gridOptions.columnDefs = response.columns.map(function(c) {
                    return {
                        name: c.title,
                        field: c.name,
                    };
                });

                var buttons = {
                    name: '',
                    field: 'buttons',
                    cellEditableCondition: false,
                    cellTemplate: '\
                      <div class="ui-grid-cell-contents ng-binding ng-scope">\
                        <button class="btn btn-danger btn-xs" ng-click="getExternalScopes().delete($event, row)"><span class="glyphicon glyphicon-trash"></span></button>\
                        <button class="btn btn-info btn-xs" ng-click="getExternalScopes().edit($event, row)"><span class="glyphicon glyphicon-pencil"></span></button>\
                      </div>'
                };

                $scope.gridOptions.columnDefs.push(buttons);

                loadData();

            });
        }

        function loadData() {

        }

        initializeGrid();
    });