'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:DoubleEditor
 * @description
 * # DoubleEditor
 */
angular.module('webAppApp')
  .directive('DoubleEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the DoubleEditor directive');
      }
    };
  });
