'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:urlEditor
 * @description
 * # urlEditor
 */
angular.module('webAppApp')
    .directive('urlEditor', function() {
        return {
            template: '<input ng-required="ngRequired" ng-disabled="ngDisabled" type="url" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });