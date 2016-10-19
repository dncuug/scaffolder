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

        var ColumnType = {
            Text: 10,
            Email: 11,
            Url: 12,
            Phone: 13,
            HTML: 14,
            Password: 15,
            Date: 20,
            Time: 21,
            DateTime: 22,
            File: 30,
            Integer: 40,
            Double: 50,
            Image: 60,
            Binary: 70,
            Lookup: 80,
            Boolean: 90
        };

        $scope.loading = false;

        $scope.gridOptions = {
            enableSorting: true,
            paginationPageSizes: [10, 20, 30, 50, 75, 100],
            paginationPageSize: 10,
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
                        paginationOptions.sortField = sortColumns[0].colDef.field;
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
                    getPage();
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

        $scope.createNew = function() {
            var url = "/detail/" + $routeParams.table;
            $location.path(url).search({ new: true });
        }

        function filterGridColumns(c) {
            return !!c.showInGrid && !c.reference;
        }

        function mapGridColumn(c) {
            var column = {
                name: c.title,
                field: c.name,
            };

            if (c.type == ColumnType.Boolean) {
                column.type = 'boolean';
                column.cellTemplate = '<input type="checkbox" disabled ng-model="!!row.entity[\'' + column.name + '\']" />';
            }

            if (c.type == ColumnType.Date) {

                column.type = 'date';
                column.cellFilter = 'date:"yyyy-MM-dd"';
                column.filter = {
                    condition: uiGridConstants.filter.STARTS_WITH,
                    placeholder: 'starts with'
                }
            }

            return column;
        }

        function initializeGrid() {

            var name = $routeParams.table;

            api.getTable(name).then(function(table) {

                $scope.table = table;
                $scope.filter.tableName = table.name;

                $scope.gridOptions.columnDefs = table.columns.filter(filterGridColumns).map(mapGridColumn);

                var references = table.columns.filter(function(o) { return !!o.reference; })

                if (!!references) {
                    references.forEach(function(v) {
                        if (!!v.showInGrid) {
                            var c = {
                                name: v.title,
                                field: v.reference.table + "_" + v.reference.textColumn,
                            };

                            $scope.gridOptions.columnDefs.push(c);
                        }
                    });
                }

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

                getPage();

            });
        }

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
            $scope.filter.sortColumn = paginationOptions.sortField;

            $scope.loading = true;

            api.select($scope.filter).then(function(response) {
                $scope.gridOptions.totalItems = response.totalItemsCount;
                $scope.gridOptions.data = response.items;
                $scope.loading = false;
            });
        };

        initializeGrid();
    });