'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:BinaryEditor
 * @description
 * # BinaryEditor
 */
angular.module('webAppApp')
  .directive('BinaryEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the BinaryEditor directive');
      }
    };
  });
