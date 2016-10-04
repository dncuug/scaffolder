'use strict';

describe('Service: authProvider', function () {

  // load the service's module
  beforeEach(module('webAppApp'));

  // instantiate service
  var authProvider;
  beforeEach(inject(function (_authProvider_) {
    authProvider = _authProvider_;
  }));

  it('should do something', function () {
    expect(!!authProvider).toBe(true);
  });

});
