'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:DoubleEditor
 * @description
 * # DoubleEditor
 */
angular.module('webAppApp')
    .directive('doubleEditor', function() {
        return {
            template: '<div></div>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                element.text('this is the DoubleEditor directive');
            }
        };
    });