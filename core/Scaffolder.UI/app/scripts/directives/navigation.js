'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:navigation
 * @description
 * # navigation
 */
angular.module('webAppApp')
    .directive('navigation', function() {
        return {
            templateUrl: 'views/directives/navigation.html',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {},
            controller: ['$scope', '$rootScope', 'api', function($scope, $rootScope, api) {

                $scope.authorized = false;

                $rootScope.$on('reload', function(event, data) {
                    reload();
                });

                function reload() {

                    api.authorized().then(function(response) {
                        $scope.authorized = response;
                    });

                    api.getSchema().then(function(response) {
                        if (!!response) {
                            $scope.tables = response.filter(function(t) {
                                return !!t.showInList;
                            });
                        }
                    });
                }

                reload();

            }]
        };
    });