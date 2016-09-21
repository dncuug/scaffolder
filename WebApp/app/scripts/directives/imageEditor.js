'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:ImageEditor
 * @description
 * # ImageEditor
 */
angular.module('webAppApp')
  .directive('imageEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the ImageEditor directive');
      }
    };
  });
