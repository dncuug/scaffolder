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
        this.currentUser = null;


        function Url(self, relativeUrl) {
            return self.Endpoint + relativeUrl;
        }

        /**
         * 
         */
        this.getTables = function() {

            return $http({
                method: 'GET',
                url: Url(this, '/table')
            }).then(function(response) {
                return response.data;
            });

        };

        this.getStorageEndpoint = function() {
            return Url(this, '/files');
        }

        /**
         * 
         */
        this.rebuildSchema = function() {
            return $http({
                method: 'POST',
                url: Url(this, '/database')
            }).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.getDatabase = function() {
            return $http({
                method: 'GET',
                url: Url(this, '/database')
            }).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.getTable = function(name) {

            return $http({
                method: 'GET',
                url: Url(this, '/table/' + name)
            }).then(function(response) {
                return response.data;
            });

        };

        /**
         * 
         */
        this.select = function(filter) {

            return $http({
                method: 'GET',
                url: Url(this, '/data/' + name),
                params: filter
            }).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.insert = function(table, entity) {

            var payload = {
                tableName: table.name,
                entity: entity
            };

            return $http({
                method: 'POST',
                url: Url(this, '/data'),
                data: payload
            }).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.update = function(table, entity) {

            var payload = {
                tableName: table.name,
                entity: entity
            };

            return $http({
                method: 'PUT',
                url: Url(this, '/data'),
                data: payload
            }).then(function(response) {
                return response.data;
            });
        };

        /**
         * 
         */
        this.delete = function(table, entity) {

            var payload = {
                tableName: table.name,
                entity: entity
            };

            return $http({
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' },
                url: Url(this, '/data'),
                data: payload
            }).then(function(response) {
                return response.data;
            });
        };

        this.signIn = function(login, password) {
            this.currentUser = {
                login: 'test'
            };
        }

        this.signOut = function() {
            this.currentUser = null;
        }


        this.isAuthorized = function() {
            return currentUser != null;
        }
    });