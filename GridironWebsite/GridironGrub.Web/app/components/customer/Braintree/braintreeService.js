(function () {
	angular
		.module('starter')
		.service('braintreeService', ['$http', '$q', '$window', 'CustomerUrls', braintreeService])

	function braintreeService($http, $q, $window, CustomerUrls) {
		var service = {};

		service.getPaymentToken = getPaymentToken;

			function getPaymentToken() {
		        var deferred = $q.defer();

		        $http({
		            url: CustomerUrls.getPaymentToken,
		            method: 'GET'
		        }).success(function (result) {
		            deferred.resolve(result);
		        }).error(function () {
		            deferred.reject();
		        });

		        return deferred.promise;
			}

		return service;
	}

})();