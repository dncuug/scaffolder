'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:phoneEditor
 * @description
 * # phoneEditor
 */
angular.module('webAppApp')
    .directive('phoneEditor', function() {
        return {
            template: '<input  ng-required="ngRequired" ng-disabled="ngDisabled" type="date" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });