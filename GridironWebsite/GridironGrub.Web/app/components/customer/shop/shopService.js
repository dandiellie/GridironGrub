(function () {
    angular
		.module('starter')
		.service('shopService', ['$q', '$http', 'CustomerUrls', shopService])

    function shopService($q, $http, CustomerUrls) {
        var service = {};

        service.getLoginInfo = getLoginInfo;
        service.getWelcome = getWelcome;
        service.getRestaurantsFromScan = getRestaurantsFromScan;
        service.getRestaurant = getRestaurant;
        service.addItem = addItem;
        service.retireOrder = retireOrder;

        function getLoginInfo(seatId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getLoginInfo + "?seatId=" + seatId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getWelcome(seatId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getWelcome + "?seatId=" + seatId,
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
                url: CustomerUrls.getScan + "?seatId=" + seatId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getRestaurant(restId) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.getRestaurant + "?restaurantId=" + restId,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
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
        function retireOrder() {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.postRetireOrder,
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