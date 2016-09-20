'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:tables
 * @description
 * # tables
 */
angular.module('webAppApp')
  .directive('tables', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the tables directive');
      }
    };
  });
