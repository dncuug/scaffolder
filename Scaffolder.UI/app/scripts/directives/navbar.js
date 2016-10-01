'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:navbar
 * @description
 * # navbar
 */
angular.module('webAppApp')
  .directive('navbar', function () {
    return {
      templateUrl: 'views/directives/navbar.html',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {

      },
      controller: ['$scope', 'api', function ($scope, api) {

        $scope.authorized = false;

        api.authorized().then(function (response) {
          $scope.authorized = response;
        });


      }]
    };
  });
