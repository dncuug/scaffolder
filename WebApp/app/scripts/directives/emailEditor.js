'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:EmailEditor
 * @description
 * # EmailEditor
 */
angular.module('webAppApp')
    .directive('emailEditor', function() {
        return {
            template: '<input ng-disabled="ngDisabled" type="email" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });