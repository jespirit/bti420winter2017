(function () {
    'use strict';

    // Declare app level module which depends on views, and core components
    angular.module('invoiceApp', [])
        .controller('invoiceController', ['$scope', 'invoiceFactory', function ($scope, invoiceFactory) {
            $scope.invoice = {
                invoiceId: 0,
                customerId: 0,
                invoiceDate: null,
                billingAddress: '',
                billingCity: '',
                billingState: '',
                billingCountry: '',
                billingPostalCode: '',
                total: 0,
                customer: null,
                invoiceLines: []
            };

            $scope.slider = {
                min: 0,
                max: 100,
                step: 0.01,
                precision: 2
            };

            $scope.initialize = function (id) {
                invoiceFactory.get(id)
                    .success(function (o) {
                        $scope.invoice = {
                            invoiceId: o.invoiceId,
                            customerId: o.customerId,
                            invoiceDate: o.invoiceDate,
                            billingAddress: o.billingAddress,
                            billingCity: o.billingCity,
                            billingState: o.billingState,
                            billingCountry: o.billingCountry,
                            billingPostalCode: o.billingPostalCode,
                            total: o.total,
                            customer: o.customer,
                            invoiceLines: o.invoiceLines
                        };
                    })
                    // FIXME: Returns 404 Error not found
                    .error(function (data, status, headers, config) {
                        console.log(data, status, headers, config);
                    });

            };
        }])
        .factory('invoiceFactory', ['$http', function ($http) {

            var urlBase = '/api/invoice';
            var invoiceFactory = {};

            invoiceFactory.get = function (id) {
                return $http.get(urlBase + '/get/?id=' + id);
            };

            return invoiceFactory;
        }]);

    // Define modules first before manually bootstrapping
    // same as calling jQuery's init callback: $(function() {})
    // Note: Need to include all modules to be loaded into the application
    angular.element(function () {
        angular.bootstrap(document.getElementById('invoiceApp'), ['invoiceApp', 'ui.bootstrap-slider']);
    });
})();