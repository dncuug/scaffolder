'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:ImageEditor
 * @description
 * # ImageEditor
 */
angular.module('webAppApp')
    .directive('binaryImageEditor', function() {
        return {
            templateUrl: 'views/directives/binaryImageEditor.html',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            },
            controller: ['$scope', 'api', 'FileUploader', function($scope, api, FileUploader) {
              
                $scope.showProgress = false;
                $scope.configuration = '';

                function reload() {
                    
                    

                }

                reload();
            }]
        };
    });