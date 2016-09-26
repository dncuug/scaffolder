'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:dateEditor
 * @description
 * # dateEditor
 */
angular.module('webAppApp')
    .directive('dateEditor', function() {
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