'use strict';

/**
 * @ngdoc service
 * @name webAppApp.api
 * @description
 * # api
 * Service in the webAppApp.
 */
angular.module('webAppApp')
  .service('api', function ($http) {

    this.Endpoint = 'http://localhost:5070/api';
    //this.Endpoint = 'http://x.mh.agi.net.ua/api';
    this.tokenKey = 'scaffolder-access-token';

    this.authorized = function () {
      return $http({
        url: this.Endpoint + '/System',
        method: 'GET',
        headers: {
          'Authorization': "Bearer " + this.getToken()
        }
      })
        .then(function (resposne) {
          return true;
        }, function (resposne) {
          return false;
        });
    };

    /**
     * Set auth token
     */
    this.setToken = function (token) {
      localStorage[this.tokenKey] = token;
      $http.defaults.headers.common.Authorization = "Bearer " + token;
    };

    /**
     * Return current auth token
     */
    this.getToken = function () {
      var token = localStorage[this.tokenKey];
      return token == "null" ? null : token;
    };

    this.signOut = function () {
      this.setToken('');
    };

    this.restart = function () {
      return this.execute('GET', '/system/restart');
    };

    /**
     * Authorize and save auth token
     */
    this.signIn = function (login, password) {

      var payload = {
        username: login,
        password: password
      };

      var self = this;

      return $http({
        method: 'POST',
        url: self.Endpoint + '/token',
        headers: {'Content-Type': 'application/x-www-form-urlencoded'},
        data: payload,
        transformRequest: function (obj) {
          var str = [];
          for (var p in obj)
            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
          return str.join("&");
        }
      }).then(function (response) {
          //Auth ok
          var token = response.data.access_token;
          self.setToken(token);
          return token;
        },
        function (data) {
          //Auth fail
          self.setToken('');
          return null;
        });
    };

    /**
     * Execute API method
     */
    this.execute = function (method, path, payload) {

      var self = this;

      var url = self.Endpoint + path;

      payload = $.isEmptyObject(payload) ? null : payload;

      if (method == "POST") {
        payload = !!payload ? JSON.stringify(payload) : payload;
      }

      var data = null;
      var params = null;

      if (method == 'GET') {
        params = payload;
      } else {
        data = payload;
      }

      return $http({
        url: url,
        method: method,
        data: data,
        params: params,
        processData: true,
        contentType: false,

        headers: {
          'Authorization': "Bearer " + self.getToken(),
          'Content-Type': 'application/json'
        }
      })
        .then(function (resposne) {
          return resposne.data;
        }, function (resposne) {
          return null;
        });
    };


    /**
     *
     */
    this.getSchema = function () {
      return this.execute('GET', '/table');
    };

    this.getStorageEndpoint = function () {
      return this.Endpoint + '/files';
    }

    /**
     *
     */
    this.rebuildSchema = function () {
      return this.execute('POST', '/database');
    };

    /**
     *
     */
    this.getTable = function (name) {
      return this.execute('GET', '/table/' + name);
    };

    /**
     *
     */
    this.select = function (filter) {

      return this.execute('GET', '/data/' + name, filter);
    };

    /**
     *
     */
    this.insert = function (table, entity) {

      var payload = {
        tableName: table.name,
        entity: entity
      };

      return this.execute('POST', '/data', payload);
    };

    /**
     *
     */
    this.update = function (table, entity) {

      var payload = {
        tableName: table.name,
        entity: entity
      };

      return this.execute('PUT', '/data', payload);

    };

    /**
     *
     */
    this.delete = function (table, entity) {

      var payload = {
        tableName: table.name,
        entity: entity
      };

      return this.execute('DELETE', '/data', payload);
    };
  });
