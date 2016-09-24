'use strict';

/**
 * @ngdoc directive
 * @name webAppApp.directive:lookupEditor
 * @description
 * # lookupEditor
 */
angular.module('webAppApp')
    .directive('lookupEditor', function() {
        return {
            template: '<select ng-required="ngRequired" ng-disabled="ngDisabled" >\
                        <option value="volvo">Volvo</option>\
                        <option value="saab">Saab</option>\
                        <option value="mercedes">Mercedes</option>\
                        <option value="audi">Audi</option>\
                      </select>',
            scope: {
                ngModel: '=',
                ngDisabled: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });