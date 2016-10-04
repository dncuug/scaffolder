'use strict';

describe('Controller: AdministrationCtrl', function () {

  // load the controller's module
  beforeEach(module('webAppApp'));

  var AdministrationCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    AdministrationCtrl = $controller('AdministrationCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(AdministrationCtrl.awesomeThings.length).toBe(3);
  });
});
