'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:booleanEditor
 * @description
 * # booleanEditor
 */
angular.module('webAppApp')
  .directive('booleanEditor', function () {
    return {
            template: '<div class="form-control">\
                          <input ng-required="ngRequired" ng-disabled="ngDisabled" type="checkbox" ng-model="ngModel" />\
                       </div>',
            scope: {
                ngModel: '=',
                ngDisabled: '=',
                maxLength: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {}
        };
  });
