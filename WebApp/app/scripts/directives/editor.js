'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:Editor
 * @description
 * # Editor
 */
angular.module('webAppApp')
    .directive('editor', function() {

        var ColumnType = {
            Text: 10,
            Email: 11,
            Url: 12,
            Phone: 13,
            HTML: 14,
            Password: 15,
            Date: 20,
            Time: 21,
            DateTime: 22,
            File: 30,
            Integer: 40,
            Double: 50,
            Image: 60,
            Binary: 70,
            Lookup: 80
        };

        return {
            templateUrl: 'views/directives/editor.html',
            restrict: 'E',
            scope: {
                ngModel: '=',
                type: '=',
                minValue: '=',
                maxValue: '=',
                maxLenght: '=',
                ngDisabled: '=',
            },

            link: function postLink(scope, element, attrs) {

            },
            controller: ['$scope', 'api', function($scope, api) {
                $scope.columnType = ColumnType;
            }]
        };
    });