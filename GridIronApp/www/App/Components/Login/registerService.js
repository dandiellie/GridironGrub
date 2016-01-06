( function() {

	angular
		.module('starter')
		.factory('registerService', ['$http', '$q', '$window', registerService]);

	function registerService($http, $q, $window) {

		var service = {};

		service.register = register;

        function register(vm) {
            var deferred = $q.defer();
            $http({
                url: 'http://localhost:49325/api/Account/Register',
                method: 'POST',
                data: vm
            }).success(function (data) {
                deferred.resolve();
            }).error(function (data) {
                deferred.reject(data);
            });

            return deferred.promise;
        }

        return service;
	}

})();