'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:DateTimeEditor
 * @description
 * # DateTimeEditor
 */
angular.module('webAppApp')
    .directive('dateTimeEditor', function() {
        return {
            template: '<input ng-disabled="ngDisabled" type="datetime" class="form-control" ng-model="ngModel" />',
           scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });