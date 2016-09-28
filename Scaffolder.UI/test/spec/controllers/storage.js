'use strict';

describe('Controller: StorageCtrl', function () {

  // load the controller's module
  beforeEach(module('webAppApp'));

  var StorageCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    StorageCtrl = $controller('StorageCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(StorageCtrl.awesomeThings.length).toBe(3);
  });
});
