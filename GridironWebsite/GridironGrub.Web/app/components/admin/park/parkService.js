(function () {
    angular
		.module('starter')
		.service('parkService', ['$q', '$http', 'AdminUrls', parkService])

    function parkService($q, $http, AdminUrls) {
        var service = {};

        service.getPark = getPark;
        service.getVendor = getVendor;
        service.getSeats = getSeats;
        service.postSeat = postSeat;
        service.updatePark = updatePark;
        service.saveArea = saveArea;
        service.saveVendor = saveVendor;
        service.saveCategory = saveCategory;
        service.saveItem = saveItem;

        function getPark() {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getPark,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getVendor(restId) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getVendor + "?restId=" + restId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getSeats() {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getSeats,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function postSeat(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postSeat,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function updatePark(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postPark,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function saveArea(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postArea,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function saveVendor(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postVendor,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function saveCategory(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postCategory,
                method: 'POST',
                data: vm
            }).success(function (result) {
                deferred.resolve();
            }).error(function (result) {
                deferred.reject();
            });

            return deferred.promise;
        }
        function saveItem(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postItem,
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