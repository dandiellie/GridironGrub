(function () {
    angular
		.module('starter')
		.service('managerService', ['$q', '$http', 'ManagerUrls', managerService])

    function managerService($q, $http, ManagerUrls) {
        var service = {};

        service.getVendors = getVendors;
        service.getVendor = getVendor;

        function getVendors() {
            var deferred = $q.defer();

            $http({
                url: ManagerUrls.getVendors,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getVendor(vendorId) {
            var deferred = $q.defer();

            $http({
                url: ManagerUrls.getVendor + "?vendorId=" + vendorId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }

        return service;
    }

})();