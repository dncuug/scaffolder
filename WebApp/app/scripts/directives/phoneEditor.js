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
            template: '<input ng-required="ngRequired" ng-disabled="ngDisabled" type="tel" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });