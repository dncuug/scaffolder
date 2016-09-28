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
      // template: '<button type="button" class="btn btn-default" ng-click="showFileUploadDialog()">Open me!</button>',
      templateUrl: 'views/directives/fileUploadDialog.html',
      restrict: 'E',
      link: function postLink(scope, element, attrs) {

      },
      controller: ['$scope', '$uibModal', 'FileUploader', 'api', function ($scope, $uibModal, FileUploader, api) {

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

        uploader.onBeforeUploadItem = function (item) {
          $scope.showProgress = true;
        };

        uploader.onErrorItem = function (fileItem, response, status, headers) {
          $scope.uploadedFileUrl = response;
        };

        uploader.onCompleteItem = function (fileItem, response, status, headers) {
          $scope.uploadedFileUrl = response;
          $scope.showProgress = false;
        };

        $scope.showFileUploadDialog = function () {

          var scope = $scope;
          //debugger;
          var dialog = $uibModal.open({
            //templateUrl: 'views/directives/fileUploadDialog.html',
            templateUrl: 'fileUploadModalDialog.html',
            animation: false,
            size: 'md',
            scope: scope
          });


        }
        // var $ctrl = this;

        // $scope.$ctrl = this;

        // $ctrl.items = ['item1', 'item2', 'item3'];

        // $ctrl.animationsEnabled = true;

        // $ctrl.open = function (size) {
        //   var modalInstance = $uibModal.open({
        //     animation: $ctrl.animationsEnabled,
        //     ariaLabelledBy: 'modal-title',
        //     ariaDescribedBy: 'modal-body',
        //     templateUrl: 'fileUploadDialog.html',
        //     //controller: 'ModalInstanceCtrl',
        //     //controllerAs: '$ctrl',
        //     size: size,
        //     resolve: {
        //       items: function () {
        //         return $ctrl.items;
        //       }
        //     }
        //   });

        //   modalInstance.result.then(function (selectedItem) {
        //     $ctrl.selected = selectedItem;
        //   }, function () {
        //     $log.info('Modal dismissed at: ' + new Date());
        //   });
        // };

        // $ctrl.toggleAnimation = function () {
        //   $ctrl.animationsEnabled = !$ctrl.animationsEnabled;
        // };
        // });

        // // Please note that $uibModalInstance represents a modal window (instance) dependency.
        // // It is not the same as the $uibModal service used above.

        // angular.module('ui.bootstrap.demo').controller('ModalInstanceCtrl', function ($uibModalInstance, items) {
        //   var $ctrl = this;
        //   $ctrl.items = items;
        //   $ctrl.selected = {
        //     item: $ctrl.items[0]
        //   };

        //   $ctrl.ok = function () {
        //     $uibModalInstance.close($ctrl.selected.item);
        //   };

        //   $ctrl.cancel = function () {
        //     $uibModalInstance.dismiss('cancel');
        //   };
        // });

        // // Please note that the close and dismiss bindings are from $uibModalInstance.

        // angular.module('ui.bootstrap.demo').component('modalComponent', {
        //   templateUrl: 'myModalContent.html',
        //   bindings: {
        //     resolve: '<',
        //     close: '&',
        //     dismiss: '&'
        //   },
        //   controller: function () {
        //     var $ctrl = this;

        //     $ctrl.$onInit = function () {
        //       $ctrl.items = $ctrl.resolve.items;
        //       $ctrl.selected = {
        //         item: $ctrl.items[0]
        //       };
        //     };

        //     $ctrl.ok = function () {
        //       $ctrl.close({ $value: $ctrl.selected.item });
        //     };

        //     $ctrl.cancel = function () {
        //       $ctrl.dismiss({ $value: 'cancel' });
        //     };
        //   }
        // });

      }]
    };
  });
