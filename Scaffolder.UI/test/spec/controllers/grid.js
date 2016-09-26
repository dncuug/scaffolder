'use strict';

describe('Controller: GridCtrl', function () {

  // load the controller's module
  beforeEach(module('webAppApp'));

  var GridCtrl,
    scope;

  // Initialize the controller and a mock scope
  beforeEach(inject(function ($controller, $rootScope) {
    scope = $rootScope.$new();
    GridCtrl = $controller('GridCtrl', {
      $scope: scope
      // place here mocked dependencies
    });
  }));

  it('should attach a list of awesomeThings to the scope', function () {
    expect(GridCtrl.awesomeThings.length).toBe(3);
  });
});
