'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:FileEditor
 * @description
 * # FileEditor
 */
angular.module('webAppApp')
  .directive('fileEditor', function () {
    return {
      templateUrl: 'views/directives/fileEditor.html',
      scope: {
        ngModel: '=',
        ngDisabled: '=',
        filesLocationUrl: '=',
        filesUploadUrl: '='
      },
      restrict: 'E',
      link: function postLink(scope, element, attrs) {

      },
      controller: ['$scope', 'api', function ($scope, api) {
        $scope.fileUrl = '';

        function reload() {
          api.getConfiguration().then(function (response) {
            $scope.configuration = response.name;
          })
        }

        function isUrl(str) {
          return !!str && str.indexOf('http') > -1;
        }

        function updateFileUrl() {

          if (!$scope.ngModel) {
            $scope.fileUrl = '';
            imageUrl
          }

          $scope.fileUrl = !!isUrl($scope.ngModel)
            ? $scope.ngModel
            : api.getStorageEndpoint() + '?name=' + $scope.ngModel + '&configuration=' + $scope.configuration;
        }

        reload();

        $scope.$watch('configuration', function (o, n) {
          if (o != n) {
            updateFileUrl();
          }
        });

        $scope.$watch('ngModel', function (o, n) {
          if (o != n) {
            updateFileUrl();
          }
        });

      }]
    };
  });
