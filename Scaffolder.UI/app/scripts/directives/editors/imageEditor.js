'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:ImageEditor
 * @description
 * # ImageEditor
 */
angular.module('webAppApp')
    .directive('imageEditor', function () {
        return {
            template: '<div>\
                        <input  ng-required="ngRequired" ng-disabled="ngDisabled" type="file" class="form-control" ng-model="ngModel" />\
                        <br />\
                        <img src="{{imageUrl}}" class="img-resposnsive" />\
                       </div>',
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