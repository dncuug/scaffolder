'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:timeEditor
 * @description
 * # timeEditor
 */
angular.module('webAppApp')
    .directive('timeEditor', function() {
        return {
            template: '<input no-validation-message="true" ng-required="ngRequired" ng-disabled="ngDisabled" type="time" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });