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
            template: '<div></div>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                element.text('this is the HtmlEditor directive');
            }
        };
    });