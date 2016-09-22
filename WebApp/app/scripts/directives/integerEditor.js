'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:IntegerEditor
 * @description
 * # IntegerEditor
 */
angular.module('webAppApp')
    .directive('integerEditor', function() {
        return {
            template: '<input ng-disabled="ngDisabled" type="number" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });