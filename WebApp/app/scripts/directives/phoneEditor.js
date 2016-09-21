'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:PhoneEditor
 * @description
 * # PhoneEditor
 */
angular.module('webAppApp')
  .directive('PhoneEditor', function () {
    return {
      template: '<div></div>',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {
        element.text('this is the PhoneEditor directive');
      }
    };
  });
