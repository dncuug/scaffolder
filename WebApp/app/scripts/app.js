'use strict';

/**
 * @ngdoc overview
 * @name webAppApp
 * @description
 * # webAppApp
 *
 * Main module of the application.
 */
angular
    .module('webAppApp', [
        'ngAnimate',
        'ngCookies',
        'ngResource',
        'ngRoute',
        'ngSanitize',
        'ngTouch',
        'ui.grid',
        'ui.grid.edit',
        'ui.grid.pagination',
        'textAngular',
        'validation',
        'validation.rule'
    ])
    .config(function($routeProvider, $validationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'views/main.html',
                controller: 'MainCtrl',
                controllerAs: 'main'
            })
            .when('/about', {
                templateUrl: 'views/about.html',
                controller: 'AboutCtrl',
                controllerAs: 'about'
            })
            .when('/grid/:table', {
                templateUrl: 'views/grid.html',
                controller: 'GridCtrl',
                controllerAs: 'grid'
            })
            .when('/detail/:table', {
                templateUrl: 'views/detail.html',
                controller: 'DetailCtrl',
                controllerAs: 'detail'
            })
            .when('/login', {
                templateUrl: 'views/login.html',
                controller: 'LoginCtrl',
                controllerAs: 'login'
            })
            .when('/administration', {
                templateUrl: 'views/administration.html',
                controller: 'AdministrationCtrl',
                controllerAs: 'administration'
            })
            .otherwise({
                redirectTo: '/'
            });




        $validationProvider.setErrorHTML(function(msg) {
            return "<label class=\"control-label has-error\">" + msg + '222222' + "</label>";
        });


        angular.extend($validationProvider, {
            validCallback: function(element) {
                $(element).parents('.form-group:first').removeClass('has-error');
            },
            invalidCallback: function(element) {
                $(element).parents('.form-group:first').addClass('has-error');
            }
        });
    });