'use strict';

/**
 * @ngdoc service
 * @name webAppApp.api
 * @description
 * # api
 * Service in the webAppApp.
 */
angular.module('webAppApp')
    .service('api', function($http) {

        this.Endpoint = 'http://localhost:5000';

        function Url(self, relativeUrl) {
            return self.Endpoint + relativeUrl;
        }


        /**
         * 
         */
        this.getTables = function() {
            return $http.get(Url(this, '/table')).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.getDatabase = function() {
            return $http.get(Url(this, '/database')).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.getTable = function(name) {
            return $http.get(Url(this, '/table/' + name)).then(function(response) {
                return response.data;
            });
        };


    });