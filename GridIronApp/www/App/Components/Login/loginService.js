( function() {
	angular
		.module('starter')
		.factory('loginService', ['$http', '$q', '$window', loginService])

	function loginService($http, $q, $window) {
        var service = {};
        service.login = login;
        // service.isLoggedIn = isLoggedIn;
        // service.logout = logout;
        // service.register = register;

        // function register(email, password, confirmPassword) {
        //     var deferred = $q.defer();
        //     $http({
        //         url: '/api/Account/Register',
        //         method: 'POST',
        //         data: {
        //             'email': email, 'password': password, 'confirmPassword': confirmPassword
        //         }
        //     }).success(function (data) {
        //         deferred.resolve();
        //     }).error(function (data) {
        //         deferred.reject(data);
        //     });

        //     return deferred.promise;
        // }

        // function logout() {
        //     $window.sessionStorage.removeItem('token');
        // }

        // function isLoggedIn() {
        //     return $window.sessionStorage.getItem('token');
        // }

        function login(username, password) {
            var deferred = $q.defer();

            $http({
                url: 'http://localhost:49325/Token',
                method: 'POST',
                data: 'username=' + username + '&password=' + password + '&grant_type=password',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                $window.sessionStorage.setItem('token', data.access_token);
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        }

        return service;
    }

})();