( function() {
	angular
		.module('starter')
		.factory('profileService', ['$http', '$q', '$window', 'CustomerUrls', profileService])
		.factory('editProfileService', ['$http', '$q', '$window', 'CustomerUrls', editProfileService]);

	function profileService($http, $q, $window, CustomerUrls) {
		service = {};
		service.getProfile = getProfile;

			function getProfile() {
	            var deferred = $q.defer();

	            $http({
	                url: 'http://localhost:49325/api/customer/profile',
	                method: 'GET'
	            }).success(function (result) {

	            	console.log(result);

	            	deferred.resolve(result);

	            }).error(function () {
	                deferred.reject();
	            });

	            return deferred.promise;
	        }

		return service;
	}

	function editProfileService($http, $q, $window, CustomerUrls) {
		var service = {};
		service.updateProfile = updateProfile;

			function updateProfile(vm) {
	            var deferred = $q.defer();

	            $http({
	                url: CustomerUrls.postProfile,
	                method: 'POST',
	                data: vm
	            }).success(function (result) {
	                deferred.resolve();
	            }).error(function (result) {
	                deferred.reject();
	            });

	            return deferred.promise;
	        }

        return service;
	}

})();
