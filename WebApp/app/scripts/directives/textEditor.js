'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:TextEditor
 * @description
 * # TextEditor
 */
angular.module('webAppApp')
    .directive('TextEditor', function() {
        return {
            template: '<input type="text" class="form-control" ng-model="ngModel" />',
            scope: {
              ngModel: '@'
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                element.text('this is the TextEditor directive');
            }
        };
    });