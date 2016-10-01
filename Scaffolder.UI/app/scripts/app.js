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

    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize',
    'ngTouch',
    'ui.grid',
    'ui.grid.edit',
    'ui.grid.pagination',
    'textAngular',
    'ui.select',
    'angularFileUpload',
    'ui.bootstrap',
    'ngclipboard'
  ])
  .config(function ($routeProvider) {

    var onlyLoggedIn = function ($location, $q, api) {

      var deferred = $q.defer();

      api.authorized().then(function (response) {
        if (response) {
          deferred.resolve();
        }
        else {
          debugger;
          deferred.reject();
          $location.url('/login');
          location.reload();
        }
      });

      return deferred.promise;
    };

    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl',
        controllerAs: 'main',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .when('/about', {
        templateUrl: 'views/about.html',
        controller: 'AboutCtrl',
        controllerAs: 'about',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .when('/grid/:table', {
        templateUrl: 'views/grid.html',
        controller: 'GridCtrl',
        controllerAs: 'grid',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .when('/detail/:table', {
        templateUrl: 'views/detail.html',
        controller: 'DetailCtrl',
        controllerAs: 'detail',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .when('/login', {
        templateUrl: 'views/login.html',
        controller: 'LoginCtrl',
        controllerAs: 'login'
      })
      .when('/administration', {
        templateUrl: 'views/administration.html',
        controller: 'AdministrationCtrl',
        controllerAs: 'administration',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .when('/storage', {
        templateUrl: 'views/storage.html',
        controller: 'StorageCtrl',
        controllerAs: 'storage',
        resolve: {loggedIn: onlyLoggedIn}
      })
      .otherwise({
        redirectTo: '/',
        resolve: {loggedIn: onlyLoggedIn}
      });
  });
