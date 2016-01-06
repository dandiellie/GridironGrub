(function () {
	angular
		.module('starter')
		.service('itemService', ['$q', '$http', 'CustomerUrls', itemService])

	function itemService($q, $http, CustomerUrls) {
		var service = {};
		service.getRestaurant = getRestaurant;

		function getRestaurant(restId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getRestaurant + "?restaurantId=" + restId,
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