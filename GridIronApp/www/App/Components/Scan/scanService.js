(function() {
	angular
		.module('starter')
		.service('scanService', ['$http', '$q', '$window', scanService]);

	function scanService($http, $q, $window) {
		service = {};

		return service;
	} 
})();