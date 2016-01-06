(function () {
	'use strict';
	
	angular
        .module('GridironWeb', ['ngRoute'])
        .config(['$httpProvider', '$locationProvider', '$routeProvider', Config])
        .constant('CustomerUrls', {
            token: '/Token',
            register: '/api/Account/Register',
			getProfile: '/api/customer/profile',
			getScan: '/api/customer/scan',
			getRestaurant: '/api/customer/restaurant',
			getCart: '/api/customer/cart',
			getBadge: '/api/customer/navbarBadge',
			getPaymentToken: '/api/customer/token',
			postProfile: '/api/customer/postProfile',
			postAddItem: '/api/customer/addItem',
			postNonce: '/api/customer/purchase'
        });
})();

function request(config) {
            config.headers = config.headers || {};
            if ($window.sessionStorage.getItem('token')) {
                config.headers.Authorization = 'Bearer ' + $window.sessionStorage.getItem('token');
            }

            return config || $q.when(config);
        }

function login(username, password) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.token,
                method: 'POST',
                data: 'username=' + username + '&password=' + password + '&grant_type=password',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                $window.sessionStorage.setItem('token', data.access_token);
                deferred.resolve();
            }).error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        }

function isLoggedIn() {
            return $window.sessionStorage.getItem('token');
        }

function logout() {
            $window.sessionStorage.removeItem('token');
        }

function register(vm) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.register,
                method: 'POST',
                data: vm
            }).success(function (data) {
                deferred.resolve();
            }).error(function (data) {
                deferred.reject();
            });

            return deferred.promise;
        }

function getProfile() {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getProfile,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

function getRestaurantsFromScan(seatId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getScan + "?id=" + seatId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

function getRestaurantById(restaurantId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getRestaurant + "?id=" + restaurantId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

function getCart() {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getCart,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

function getBadge() {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getBadge,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

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

function addItem(vm) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.postAddItem,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }

function postPurchaseNonce(vm) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.postNonce,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }