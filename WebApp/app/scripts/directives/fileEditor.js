'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:FileEditor
 * @description
 * # FileEditor
 */
angular.module('webAppApp')
  .directive('fileEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the FileEditor directive');
      }
    };
  });
