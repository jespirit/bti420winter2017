(function () {
    'use strict';

    // Declare app level module which depends on views, and core components
    angular.module('invoiceApp', [])
        .controller('invoiceController', ['$scope', function ($scope) {
            $scope.slider = {
                model: 0,
                min: 0,
                max: 100,
                step: 1
            };
        }]);

    // Define modules first before manually bootstrapping
    // same as calling jQuery's init callback: $(function() {})
    // Note: Need to include all modules to be loaded into the application
    angular.element(function () {
        angular.bootstrap(document.getElementById('invoiceApp'), ['invoiceApp', 'ui.bootstrap-slider']);
    });
})();