'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:tables
 * @description
 * # tables
 */
angular.module('webAppApp')
    .directive('tables', function() {
        return {
            template: '<ul class="sidebar-menu"><li ng-repeat="t in tables"><a href="/#/grid/{{t.name}}"><i class="fa fa-table"></i> <span>{{t.title}}</span></a></li></ul>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {},
            controller: ['$scope', 'api', function($scope, api) {

                api.getTables().then(function(response) {
                    $scope.tables = response;
                });

            }]
        };
    });