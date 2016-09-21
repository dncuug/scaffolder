'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:DateEditor
 * @description
 * # DateEditor
 */
angular.module('webAppApp')
  .directive('DateEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the DateEditor directive');
      }
    };
  });
