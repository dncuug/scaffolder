'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:DateTimeEditor
 * @description
 * # DateTimeEditor
 */
angular.module('webAppApp')
    .directive('dateTimeEditor', function() {
        return {
            template: '<div></div>',
            restrict: 'E',
            link: function postLink(scope, element, attrs) {
                element.text('this is the DateTimeEditor directive');
            }
        };
    });