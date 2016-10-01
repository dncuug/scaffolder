'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:fileUploadDialog
 * @description
 * # fileUploadDialog
 */
angular.module('webAppApp')
  .directive('fileUploadDialog', function ($uibModal) {
    return {
      templateUrl: 'views/directives/fileUploadDialog.html',
      restrict: 'E',
      link: function postLink(scope, element, attrs, api) {
      },
      controller: ['$scope', '$uibModal', 'FileUploader', 'api', function ($scope, $uibModal, FileUploader, api) {

        $scope.uploadedFileUrl = '';
        $scope.showProgress = false;

        var uploader = $scope.uploader = new FileUploader({
          url: api.getStorageEndpoint(),
          removeAfterUpload: true,
          autoUpload: true
        });

        // FILTERS

        uploader.filters.push({
          name: 'customFilter',
          fn: function (item /*{File|FileLikeObject}*/, options) {
            return this.queue.length < 10;
          }
        });

        // CALLBACKS

        uploader.onBeforeUploadItem = function (item) {
          $scope.showProgress = true;
        };

        uploader.onAfterAddingFile = function (fileItem) {
          uploader.queue[uploader.queue.length - 1].headers.authorization = 'Bearer ' + api.getToken();
        };

        uploader.onErrorItem = function (fileItem, response, status, headers) {
          $scope.uploadedFileUrl = '';
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
          $scope.uploadedFileUrl = response;
          $scope.showProgress = false;
        };

        $scope.showFileUploadDialog = function () {

          var scope = $scope;

          $uibModal.open({
            templateUrl: 'fileUploadModalDialog.html',
            animation: false,
            size: 'md',
            scope: scope
          });

        };

      }]
    };
  });
