(function () {
    angular
		.module('starter')
		.service('ordersService', ['$q', '$http', 'AdminUrls', ordersService])

    function ordersService($q, $http, AdminUrls) {
        var service = {};

        service.getOpen = getOpen;
        service.getRecent = getRecent;
        service.getSpecific = getSpecific;

        function getOpen() {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getOpen,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getRecent(date) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getRecent + "?date=" + date,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getSpecific(orderId) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getSpecific + "?orderId=" + orderId,
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