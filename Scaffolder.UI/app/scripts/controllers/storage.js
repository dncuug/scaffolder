'use strict';

/**
 * @ngdoc function
 * @name webAppApp.controller:StorageCtrl
 * @description
 * # StorageCtrl
 * Controller of the webAppApp
 */
angular.module('webAppApp')
    .controller('StorageCtrl', function ($scope, FileUploader, api) {

        $scope.uploadedFileUrl = '';
        $scope.showProgress = false;

        var uploader = $scope.uploader = new FileUploader({
            url: api.getUploadEndpoint(),
            removeAfterUpload: true
        });

        // FILTERS

        uploader.filters.push({
            name: 'customFilter',
            fn: function (item /*{File|FileLikeObject}*/, options) {
                return this.queue.length < 10;
            }
        });

        // CALLBACKS

        uploader.onWhenAddingFileFailed = function (item /*{File|FileLikeObject}*/, filter, options) {
        };

        uploader.onAfterAddingFile = function (fileItem) {
        };

        uploader.onAfterAddingAll = function (addedFileItems) {
        };

        uploader.onBeforeUploadItem = function (item) {
            $scope.showProgress = true;
        };

        uploader.onProgressItem = function (fileItem, progress) {
        };

        uploader.onProgressAll = function (progress) {
        };

        uploader.onSuccessItem = function (fileItem, response, status, headers) {
        };

        uploader.onErrorItem = function (fileItem, response, status, headers) {
            $scope.uploadedFileUrl = response;
        };

        uploader.onCancelItem = function (fileItem, response, status, headers) {
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
            $scope.uploadedFileUrl = response;
            $scope.showProgress = false;
        };

        uploader.onCompleteAll = function (result) {
        };

    });