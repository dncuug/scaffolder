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

    this.Endpoint = 'http://localhost:5000';
    this.currentUser = null;

    this.tokenKey = "scaffolder-access-token";

    /**
     * Set auth token
     */
    this.setToken = function (token) {
      localStorage[this.tokenKey] = token;
    };

    /**
     * Return current auth token
     */
    this.getToken = function () {
      var token = localStorage[this.tokenKey];
      return token == "null" ? null : token;
    };


    this.logout = function () {
      localStorage[this.tokenKey] = '';
    };

    /**
     * Authorize and save auth token
     */
    this.auth = function (login, password) {

      var payload = {
        username: login,
        password: password,
      };

      var self = this;

      var d = $.Deferred();

      $http({
        method: 'POST',
        url: self.Endpoint + '/token',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        transformRequest: function (obj) {
          var str = [];
          for (var p in obj)
            str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
          return str.join("&");
        },
        data: payload,
        success: function (response) {
          self.setToken(response.access_token);
        },
        error: function (xhr, status, err) {
          console.warn(xhr, status, err.toString());

          self.logout();
          d.resolve(null);
        }
      });

      return d.promise();
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

      var jqxhr = $http({
        url: url,
        method: method,
        data: data,
        params: params,
        processData: true,
        contentType: false,
        beforeSend: function (xhr) {
          xhr.setRequestHeader("Accept", "application/json");
          xhr.setRequestHeader("Content-Type", "application/json");

          if (!!self.getToken()) {
            xhr.setRequestHeader("Authorization", "Bearer " + self.getToken());
          }
        },
        success: function () {
          //console.info('Request to campus API success: ', response);
        },
        error: function (jqXHR, status, error) {
          console.warn('Error occured: ', status, error);
        }
      });

      return jqxhr.then(function (resposne) {
        return resposne.data;
      });
    };


    /**
     *
     */
    this.getTables = function () {
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
    this.getDatabase = function () {
      return this.execute('GET', '/database');
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
