'use strict';

describe('Directive: binaryImageEditor', function () {

  // load the directive's module
  beforeEach(module('webAppApp'));

  var element,
    scope;

  beforeEach(inject(function ($rootScope) {
    scope = $rootScope.$new();
  }));

  it('should make hidden element visible', inject(function ($compile) {
    element = angular.element('<binary-image-editor></binary-image-editor>');
    element = $compile(element)(scope);
    expect(element.text()).toBe('this is the binaryImageEditor directive');
  }));
});
