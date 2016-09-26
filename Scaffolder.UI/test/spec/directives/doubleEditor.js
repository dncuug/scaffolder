'use strict';

describe('Directive: DoubleEditor', function () {

  // load the directive's module
  beforeEach(module('webAppApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<-double-editor></-double-editor>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the DoubleEditor directive');
  }));
});
