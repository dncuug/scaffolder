'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:referenceEditor
 * @description
 * # referenceEditor
 */
angular.module('webAppApp')
  .directive('referenceEditor', function () {
    return {
      template: '<ui-select ng-model="ngModel">\
                      <ui-select-match>\
                          <span ng-bind="$select.selected.name"></span>\
                      </ui-select-match>\
                      <ui-select-choices repeat="item in (itemArray | filter: $select.search) track by item.id">\
                          <span ng-bind="item.name"></span>\
                      </ui-select-choices>\
                  </ui-select>',
      scope: {
        ngModel: '=',
        ngDisabled: '=',
        keyColumn: '=',
        displayColumn: '=',
        table: '='
      },
      restrict: 'E',
      link: function postLink(scope, element, attrs) {

      },
      controller: ['$scope', 'api', function ($scope, api) {

        var filter = {
          pageSize: null,
          sortOrder: 1,
          sortColumn: '',
          currentPage: 1,
          parameters: [],
          tableName: $scope.table,
          detailMode: false,
        };

        api.select(filter).then(function (response) {
          $scope.itemArray = response.items.map(function (o) {
            return {
              id: o[$scope.keyColumn],
              name: o[$scope.displayColumn],
            }
          });
        });

      }]
    };
  });
