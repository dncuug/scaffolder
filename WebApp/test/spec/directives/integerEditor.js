'use strict';

describe('Directive: IntegerEditor', function () {

  // load the directive's module
  beforeEach(module('webAppApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<-integer-editor></-integer-editor>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the IntegerEditor directive');
  }));
});
