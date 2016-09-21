'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:UrlEditor
 * @description
 * # UrlEditor
 */
angular.module('webAppApp')
  .directive('UrlEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the UrlEditor directive');
      }
    };
  });
