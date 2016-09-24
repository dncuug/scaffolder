'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:GridCtrl
 * @description
 * # GridCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('GridCtrl', function($scope, $routeParams, $location, api, uiGridConstants) {

        var paginationOptions = {
            pageNumber: 1,
            pageSize: 5,
            sort: null
        };

        $scope.gridOptions = {
            enableSorting: true,
            paginationPageSizes: [5, 25, 50, 75],
            paginationPageSize: 5,
            useExternalPagination: true,
            useExternalSorting: true,
            columnDefs: [],
            data: [],
            onRegisterApi: function(gridApi) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.on.sortChanged($scope, function(grid, sortColumns) {
                    if (sortColumns.length == 0) {
                        paginationOptions.sort = null;
                    } else {
                        paginationOptions.sort = sortColumns[0].sort.direction;
                    }
                    getPage();
                });
                gridApi.pagination.on.paginationChanged($scope, function(newPage, pageSize) {
                    paginationOptions.pageNumber = newPage;
                    paginationOptions.pageSize = pageSize;
                    getPage();
                });
            }
        };

        function getPage() {
            var url;
            switch (paginationOptions.sort) {
                case uiGridConstants.ASC:
                    $scope.filter.sortOrder = 0;
                    break;
                case uiGridConstants.DESC:
                    $scope.filter.sortOrder = 1;
                    break;
                default:
                    $scope.filter.sortOrder = -1;
                    break;
            }

            $scope.filter.pageSize = $scope.gridOptions.paginationPageSize;
            $scope.filter.currentPage = $scope.gridOptions.paginationCurrentPage;


            api.select($scope.filter).then(function(response) {
                $scope.gridOptions.totalItems = response.totalItemsCount;
                $scope.gridOptions.data = response.items;
            });
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

        $scope.delete = function(e, row) {

            var r = confirm("Are you sure that you want to permanently delete the selected record?");

            if (r == true) {
                api.delete($scope.table, row.entity).then(function() {
                    var url = "/grid/" + $scope.table.name;
                    $location.path(url).search();
                });
            }
        };

        $scope.edit = function(e, row) {

            var keys = $scope.table.columns.filter(function(value, index) {
                return value.isKey == true;
            });

            var params = {};

            keys.forEach(function(key) {
                params[key.name] = row.entity[key.name];
            }, this);

            var url = "/detail/" + $routeParams.table;
            $location.path(url).search(params);
        };

        function lowercaseFirstLetter(string) {
            return string.charAt(0).toLowerCase() + string.slice(1);
        }

        $scope.createNew = function() {
            var url = "/detail/" + $routeParams.table;
            $location.path(url).search({ new: true });
        }

        function filterGridColumns(column) {
            return !!column.showInGrid;
        }

        function initializeGrid() {

            var name = $routeParams.table;

            api.getTable(name).then(function(table) {

                $scope.table = table;

                $scope.filter.tableName = table.name;

                $scope.gridOptions.columnDefs = table.columns.filter(filterGridColumns).map(function(c) {
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
                    enableHiding: false,
                    enableColumnMenu: false,
                    width: 65,
                    cellTemplate: '\
                      <div class="ui-grid-cell-contents ng-binding ng-scope">\
                        <button class="btn btn-info btn-xs" ng-click="grid.appScope.edit($event, row)"><span class="glyphicon glyphicon-pencil"></span></button>\
                        <button class="btn btn-danger btn-xs" ng-click="grid.appScope.delete($event, row)"><span class="glyphicon glyphicon-trash"></span></button>\
                      </div>'
                };

                $scope.gridOptions.columnDefs.push(buttons);

                //loadData();

                getPage();

            });
        }

        // function loadData() {

        //     $scope.filter.pageSize = $scope.gridOptions.paginationPageSize;
        //     $scope.filter.currentPage = $scope.gridOptions.paginationCurrentPage;

        //     api.select($scope.filter).then(function(response) {
        //         $scope.gridOptions.data = response.items;
        //     });
        // }

        initializeGrid();
    });