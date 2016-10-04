'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:FileEditor
 * @description
 * # FileEditor
 */
angular.module('webAppApp')
    .directive('fileEditor', function() {
        return {
            template: '<input  ng-required="ngRequired" ng-disabled="ngDisabled" type="file" class="form-control" ng-model="ngModel" />',
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
                $scope.imageUrl = $scope.staticFilesLocationUrl + $scope.ngModel;
            }]
        };
    });