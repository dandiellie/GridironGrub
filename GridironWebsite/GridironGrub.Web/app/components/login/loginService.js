(function () {
    'use strict';

    angular
        .module('starter')
        .factory('authService', ['$window', '$q', authService])
        .factory('loginService', ['$http', '$q', '$window', 'UserUrls', loginService]);

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

    function loginService($http, $q, $window, UserUrls) {
        var service = {};

        service.login = login;
        service.getRoles = getRoles;
        service.getInfoIds = getInfoIds;
        service.isLoggedIn = isLoggedIn;
        service.isAdmin = isAdmin;
        service.isManager = isManager;
        service.isVendor = isVendor;
        service.isRunner = isRunner;
        service.isSeat = isSeat;
        service.isCustomer = isCustomer;
        service.logout = logout;
        service.register = register;

        function login(username, password) {
            var deferred = $q.defer();

            $http({
                url: '/Token',
                method: 'POST',
                data: 'username=' + username + '&password=' + password + '&grant_type=password',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                $window.sessionStorage.setItem('token', data.access_token);
                if (data.isAdmin == "true") $window.sessionStorage.setItem('isAdmin', data.isAdmin);
                if (data.isManager == "true") $window.sessionStorage.setItem('isManager', data.isManager);
                if (data.isVendor == "true") $window.sessionStorage.setItem('isVendor', data.isVendor);
                if (data.isRunner == "true") $window.sessionStorage.setItem('isRunner', data.isRunner);
                if (data.isSeat == "true") $window.sessionStorage.setItem('isSeat', data.isSeat);
                if (data.isCustomer == "true") $window.sessionStorage.setItem('isCustomer', data.isCustomer);
                deferred.resolve();
            }).error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getRoles() {
            var deferred = $q.defer();

            $http({
                url: UserUrls.getRoles,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getInfoIds() {
            var deferred = $q.defer();

            $http({
                url: UserUrls.getInfoIds,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function isLoggedIn() {
            return $window.sessionStorage.getItem('token');
        }
        function isAdmin() {
            return $window.sessionStorage.getItem('isAdmin');
        }
        function isManager() {
            return $window.sessionStorage.getItem('isManager');
        }
        function isVendor() {
            return $window.sessionStorage.getItem('isVendor');
        }
        function isRunner() {
            return $window.sessionStorage.getItem('isRunner');
        }
        function isSeat() {
            return $window.sessionStorage.getItem('isSeat');
        }
        function isCustomer() {
            return $window.sessionStorage.getItem('isCustomer');
        }
        function logout() {
            $window.sessionStorage.removeItem('token');
            $window.sessionStorage.removeItem('isAdmin');
            $window.sessionStorage.removeItem('isManager');
            $window.sessionStorage.removeItem('isVendor');
            $window.sessionStorage.removeItem('isRunner');
            $window.sessionStorage.removeItem('isSeat');
            $window.sessionStorage.removeItem('isCustomer');
        }
        function register(email, password, confirmPassword) {
            var deferred = $q.defer();

            $http({
                url: '/api/Account/Register',
                method: 'POST',
                data: { 'email': email, 'password': password, 'confirmPassword': confirmPassword }
            }).success(function (data) {
                deferred.resolve();
            }).error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        }

        return service;
    }
})();