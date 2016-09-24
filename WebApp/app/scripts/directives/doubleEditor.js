'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:lookupEditor
 * @description
 * # lookupEditor
 */
angular.module('webAppApp')
    .directive('lookupEditor', function() {
        return {
            template: '<input no-validation-message="true" validator="{{validatorRules}}" ng-required="ngRequired" maxlength="maxLength" ng-disabled="ngDisabled" type="text" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '=',
                maxLength: '=',
                validatorRules: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });