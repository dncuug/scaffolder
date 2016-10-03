'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:fileUpload
 * @description
 * # fileUpload
 */
angular.module('webAppApp')
    .directive('fileUpload', function() {
        return {
            templateUrl: 'views/directives/fileUpload.html',
            restrict: 'E',
            link: function postLink(scope, element, attrs) {},
            scope: {                
            },
            controller: ['$scope', '$uibModal', 'FileUploader', 'api', function($scope, $uibModal, FileUploader, api) {

                $scope.showProgress = false;
                $scope.uploadedFileUrl = '';
                $scope.uploadedFileName = '';

                var uploader = new FileUploader({
                    url: api.getStorageEndpoint(),
                    removeAfterUpload: true,
                    autoUpload: true
                });

                $scope.uploader = uploader;

                // FILTERS

                uploader.filters.push({
                    name: 'customFilter',
                    fn: function(item /*{File|FileLikeObject}*/ , options) {
                        return this.queue.length < 10;
                    }
                });

                // CALLBACKS

                uploader.onBeforeUploadItem = function(item) {
                    $scope.showProgress = true;
                };

                uploader.onAfterAddingFile = function(fileItem) {
                    uploader.queue[uploader.queue.length - 1].headers.authorization = 'Bearer ' + api.getToken();
                };

                uploader.onErrorItem = function(fileItem, response, status, headers) {
                    $scope.uploadedFileUrl = '';
                    $scope.uploadedFileName = '';
                };

                uploader.onCompleteItem = function(fileItem, response, status, headers) {

                    $scope.uploadedFileUrl = response.url;
                    $scope.uploadedFileName = response.name;

                    $scope.showProgress = false;
                };

            }]
        };
    });