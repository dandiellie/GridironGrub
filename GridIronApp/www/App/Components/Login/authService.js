( function() {

	angular
		.module('starter')
		.factory('authService', ['$window', '$q', authService]);

	function authService($window, $q) {

		var service = {};

		service.request = request;

		 function request(config) {
            config.headers = config.headers || {};
            if ($window.sessionStorage.getItem('token')) {
                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.getItem('token');
            }

            return config || $q.when(config);
        }

        return service;
	}

})();