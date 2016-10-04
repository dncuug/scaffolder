'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:fileUploadDialog
 * @description
 * # fileUploadDialog
 */
angular.module('webAppApp')
    .directive('fileUploadDialog', function($uibModal) {
        return {
            templateUrl: 'views/directives/fileUploadDialog.html',
            restrict: 'E',
            link: function postLink(scope, element, attrs, api) {},
            controller: ['$scope', '$uibModal', 'FileUploader', 'api', function($scope, $uibModal, FileUploader, api) {

                $scope.uploadedFileUrl = '';

                $scope.showFileUploadDialog = function() {

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