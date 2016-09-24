'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:TextEditor
 * @description
 * # TextEditor
 */
angular.module('webAppApp')
    .directive('textEditor', function() {
        return {
            template: '<input ng-required="ngRequired" maxlength="maxLength" ng-disabled="ngDisabled" type="text" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '=',
                maxLength: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });