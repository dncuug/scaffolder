'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:GridCtrl
 * @description
 * # GridCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('GridCtrl', function ($scope, $routeParams, $location, api) {

        $scope.gridOptions = {
            enableSorting: true,
            paginationPageSizes: [25, 50, 75],
            paginationPageSize: 25,
            columnDefs: [],
            data: []
        };

        $scope.filter = {
            pageSize: 10,
            sortOrder: 1,
            sortColumn: '',
            currentPage: 1,
            parameters: [],
            tableName: '',
            detailMode: false,
        };

        $scope.title = '';

        $scope.delete = function (e, row) {
            debugger;
            alert('Name: ' + value);
        };

        $scope.edit = function (e, row) {

            //TODO: implement Primary key selection
            var url = "/detail/" + $routeParams.table + "/" + row.entity.Id;
            $location.path(url);
        };

        function filterGridColumns(column) {
            return !!column.showInGrid;
        }

        function initializeGrid() {

            var name = $routeParams.table;

            api.getTable(name).then(function (table) {

                $scope.filter.tableName = table.name;
                $scope.title = table.title;

                $scope.gridOptions.columnDefs = table.columns.filter(filterGridColumns).map(function (c) {
                    return {
                        name: c.title,
                        field: c.name,
                    };
                });

                var buttons = {
                    name: '',
                    field: 'buttons',
                    cellEditableCondition: false,
                    enableSorting: false,
                    cellTemplate: '\
                      <div class="ui-grid-cell-contents ng-binding ng-scope">\
                        <button class="btn btn-danger btn-xs" ng-click="grid.appScope.delete($event, row)"><span class="glyphicon glyphicon-trash"></span></button>\
                        <button class="btn btn-info btn-xs" ng-click="grid.appScope.edit($event, row)"><span class="glyphicon glyphicon-pencil"></span></button>\
                      </div>'
                };

                $scope.gridOptions.columnDefs.push(buttons);

                loadData();

            });
        }

        function loadData() {

            $scope.filter.pageSize = $scope.gridOptions.paginationPageSize;
            $scope.filter.currentPage = $scope.gridOptions.paginationCurrentPage;

            api.getData($scope.filter).then(function (response) {
                $scope.gridOptions.data = response;
            });
        }

        initializeGrid();
    });