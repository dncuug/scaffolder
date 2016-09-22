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
            Date: 20,
            Time: 21,
            DateTime: 22,
            File: 30,
            Integer: 40,
            Double: 50,
            Image: 60,
            Binary: 70
        };

        return {
            templateUrl: 'views/directives/editor.html',
            restrict: 'E',
            scope: {
                ngModel: '=',
                type: '=',
                minValue: '=',
            },
            link: function postLink(scope, element, attrs) {
                sope.columnType = ColumnType;
            }
        };
    });