(function () {
	angular
		.module('starter')
		.service('vendorsService', ['$q', '$http', 'CustomerUrls', vendorsService])

	function vendorsService($q, $http, CustomerUrls) {
		var service = {};
		service.getRestaurantsFromScan = getRestaurantsFromScan;

		function getRestaurantsFromScan(seatId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getScan + "?seatId=" + seatId,
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