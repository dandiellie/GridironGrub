(function () {
    angular
		.module('starter')
		.service('vendorService', ['$q', '$http', 'VendorUrls', vendorService])

    function vendorService($q, $http, VendorUrls) {
        var service = {};

        service.getOpen = getOpen;
        service.getRecent = getRecent;
        service.postComplete = postComplete;

        function getOpen() {
            var deferred = $q.defer();

            $http({
                url: VendorUrls.getOpen,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getRecent() {
            var deferred = $q.defer();

            $http({
                url: VendorUrls.getRecent,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function postComplete(orderId) {
            var deferred = $q.defer();

            $http({
                url: VendorUrls.postComplete + "?orderId=" + orderId,
                method: 'GET'
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