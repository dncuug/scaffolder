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
      templateUrl: 'views/directives/editor.html',
      restrict: 'E',
      scope: {
        ngModel: '='
      },
      link: function postLink(scope, element, attrs) {

      }
    };
  });
