'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:EmailEditor
 * @description
 * # EmailEditor
 */
angular.module('webAppApp')
  .directive('EmailEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the EmailEditor directive');
      }
    };
  });
