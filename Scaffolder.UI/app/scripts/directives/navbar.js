'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:navbar
 * @description
 * # navbar
 */
angular.module('webAppApp')
    .directive('navbar', function() {
        return {
            templateUrl: 'views/directives/navbar.html',
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            },
            controller: ['$scope', '$rootScope', 'api', function($scope, $rootScope, api) {

                $scope.authorized = false;

                $rootScope.$on('reload', function(event, data) {
                    reload();
                });

                function reload() {
                    api.authorized().then(function(response) {
                        $scope.authorized = response;
                    });
                }

                reload();


            }]
        };
    });