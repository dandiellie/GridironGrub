(function () {
    'use strict';
    angular
        .module('starter')
        .factory('profileService', ['$http', '$q', '$window', 'RunnerUrls', profileService])
        .factory('allOrderService', ['$http', '$q', '$window', 'RunnerUrls', allOrderService])
        .factory('activeOrderService', ['$http', '$q', '$window', 'RunnerUrls', activeOrderService])
        .factory('historyOrderService', ['$http', '$q', '$window', 'RunnerUrls', historyOrderService])
        .factory('acceptOrderService', ['$http', '$q', '$window', 'RunnerUrls', acceptOrderService])
        .factory('deliverOrderService', ['$http', '$q', '$window', 'RunnerUrls', deliverOrderService]);

    function profileService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.getProfile = getProfile;

        function getProfile() {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.getProfile,
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

    function allOrderService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.getAllOrder = getAllOrder;

        function getAllOrder() {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.getAllOrders,
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

    function activeOrderService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.getActiveOrder = getActiveOrder;

        function getActiveOrder() {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.getActiveOrders,
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

    function historyOrderService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.getHistoryOrder = getHistoryOrder;

        function getHistoryOrder() {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.getHistoryOrders,
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

    function acceptOrderService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.acceptOrder = acceptOrder;

        function acceptOrder(ordId) {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.postAcceptOrders + "?orderId=" + ordId,
                method: 'POST'
            }).success(function (result) {
                deferred.resolve();
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

        return service;
    }

    function deliverOrderService($http, $q, $window, RunnerUrls) {
        var service = {};
        service.deliverOrders = deliverOrders;

        function deliverOrders(ordId) {
            var deferred = $q.defer();

            $http({
                url: RunnerUrls.postDeliverOrders + "?orderId=" + ordId,
                method: 'POST'
            }).success(function (result) {
                deferred.resolve();
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }

        return service;
    }

})();