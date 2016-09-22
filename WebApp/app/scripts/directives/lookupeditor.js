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
            template: '<select>\
                        <option value="volvo">Volvo</option>\
                        <option value="saab">Saab</option>\
                        <option value="mercedes">Mercedes</option>\
                        <option value="audi">Audi</option>\
                      </select>',
            scope: {
                ngModel: '='
            },
            restrict: 'E',
            link: function postLink(scope, element, attrs) {

            }
        };
    });