'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:ImageEditor
 * @description
 * # ImageEditor
 */
angular.module('webAppApp')
    .directive('imageEditor', function() {
        return {
            template: '<div>\
            ngModel: {{ngModel}}\
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
            controller: ['$scope', 'api', function($scope, api) {

                function isUrl(str) {
                    return !!str && str.indexOf('http') > -1;
                }


                $scope.$watch('ngModel', function(o, n) {

                    if (o != n) {
                        debugger;
                        $scope.imageUrl = isUrl($scope.ngModel) ?
                            $scope.ngModel :
                            api.getStorageEndpoint() + '/' + $scope.ngModel;
                    }

                });

            }]
        };
    });