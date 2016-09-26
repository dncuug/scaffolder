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
      template: '<ui-select ng-model="$parent.selectedItem">\
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
        $scope.selectedItem = null;

        var filter = {
          pageSize: null,
          sortOrder: 1,
          sortColumn: '',
          currentPage: 1,
          parameters: [],
          tableName: $scope.table,
          detailMode: false,
        };

        function selectItem(id) {

        }

        api.select(filter).then(function (response) {
          $scope.itemArray = response.items.map(function (o) {
            return {
              id: o[$scope.keyColumn],
              name: o[$scope.displayColumn],
            }
          });

          setSelectedItem()
        });

        $scope.$watch('selectedItem', function (o, n) {

          if (o != n) {
            $scope.ngModel = $scope.selectedItem.id;
          }

        });

        function setSelectedItem() {

          if (!!$scope.itemArray) {
            var selectedItem = $scope.itemArray.filter(function (item) {
              return item.id == $scope.ngModel;
            });

            $scope.selectedItem = !!selectedItem ? selectedItem[0] : null;
          }
        }

        $scope.$watch('ngModel', function (o, n) {

          if (o != n) {
            setSelectedItem();
          }

        });

      }]
    };
  });
