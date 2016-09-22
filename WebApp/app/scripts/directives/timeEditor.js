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
            template: '<input type="time" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });