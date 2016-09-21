'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:TextEditor
 * @description
 * # TextEditor
 */
angular.module('webAppApp')
  .directive('TextEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the TextEditor directive');
      }
    };
  });
