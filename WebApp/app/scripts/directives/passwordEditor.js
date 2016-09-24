'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:passwordEditor
 * @description
 * # passwordEditor
 */
angular.module('webAppApp')
    .directive('passwordEditor', function() {
        return {
            template: '<input no-validation-message="true" ng-required="ngRequired" ng-disabled="ngDisabled" type="password" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });