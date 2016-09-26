'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:BinaryEditor
 * @description
 * # BinaryEditor
 */
angular.module('webAppApp')
    .directive('binaryEditor', function() {
        return {
            template: '<div></div>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                element.text('this is the BinaryEditor directive');
            }
        };
    });