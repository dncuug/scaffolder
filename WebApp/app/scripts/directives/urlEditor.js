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
            template: '<input type="url" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });