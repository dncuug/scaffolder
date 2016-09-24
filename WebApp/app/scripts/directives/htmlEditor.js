'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:HtmlEditor
 * @description
 * # HtmlEditor
 */
angular.module('webAppApp')
    .directive('htmlEditor', function() {
        return {
            template: '<div  ng-disabled="ngDisabled" text-angular ng-model="ngModel"></div>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                
            }
        };
    });