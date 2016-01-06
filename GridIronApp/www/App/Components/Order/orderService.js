(function () {
	angular
		.module('starter')
		.service('orderService', ['$q', '$http', 'CustomerUrls', orderService])

	function orderService($q, $http, CustomerUrls) {
		var service = {};

		service.getWelcome = getWelcome;

		function getWelcome(seatId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getWelcome + "?seatId=" + seatId,
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