'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:Editor
 * @description
 * # Editor
 */
angular.module('webAppApp')
  .directive('editor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the Editor directive');
      }
    };
  });
