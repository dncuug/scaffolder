'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:HtmlEditor
 * @description
 * # HtmlEditor
 */
angular.module('webAppApp')
  .directive('HtmlEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the HtmlEditor directive');
      }
    };
  });
