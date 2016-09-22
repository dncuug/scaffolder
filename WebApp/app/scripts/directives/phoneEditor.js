'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:PhoneEditor
 * @description
 * # PhoneEditor
 */
angular.module('webAppApp')
    .directive('phoneEditor', function() {
        return {
            template: '<input type="tel" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });