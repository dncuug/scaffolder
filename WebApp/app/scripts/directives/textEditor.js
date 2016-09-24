'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:TextEditor
 * @description
 * # TextEditor
 */
angular.module('webAppApp')
    .directive('textEditor', function($compile) {
        return {
            //template: '<input no-validation-message="true" validator="{{validatorRules}}" ng-required="ngRequired" maxlength="{{maxLength}}" ng-disabled="ngDisabled" type="text" class="form-control" ng-model="ngModel" />',
            //template: '<input no-validation-message="true" {{ !!validatorRules ? validator="{{ scope.validatorRules }}" : "" }}  ng-required="ngRequired" maxlength="{{maxLength}}" ng-disabled="ngDisabled" type="text" class="form-control" ng-model="ngModel" />',
            scope: {
                ngModel: '=',
                ngDisabled: '=',
                maxLength: '=',
                validatorRules: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                debugger;
                var v = !!scope.validatorRules ? 'validator="' + scope.validatorRules + '"' : '';
                var generatedTemplate = '<input no-validation-message="true" ' + v + '  ng-required="ngRequired" maxlength="{{maxLength}}" ng-disabled="ngDisabled" type="text" class="form-control" ng-model="ngModel" />';
                element.append($compile(generatedTemplate)(scope));
            }
        };
    });