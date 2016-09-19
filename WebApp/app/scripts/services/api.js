'use strict';

/**
 * @ngdoc service
 * @name webAppApp.api
 * @description
 * # api
 * Service in the webAppApp.
 */
angular.module('webAppApp')
  .service('api', function ($scope, $http) {

    this.Endpoint = '';

    function Url(relativeUrl) {
      return thid.Endpoint + relativeUrl;
    }


    /**
    * 
    */
    this.getTables = function (url) {
      $http.get(Url('/Tables'))
        .then(function (response) {
          return response.data;
        });
    };


  });
