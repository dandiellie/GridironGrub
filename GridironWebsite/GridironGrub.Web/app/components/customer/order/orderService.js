(function () {
    angular
		.module('starter')
		.service('orderService', ['$q', '$http', 'CustomerUrls', orderService])

    function orderService($q, $http, CustomerUrls) {
        var service = {};

        service.getNumItems = getNumItems;
        service.containsAlcohol = containsAlcohol;
        service.getCart = getCart;
        //service.sendReceipt = sendReceipt;
        service.getPaymentToken = getPaymentToken;
        service.sendPaymentNonce = sendPaymentNonce;

        function getNumItems() {
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
        function containsAlcohol() {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.containsAlcohol,
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
            }).error(function (result) {
                deferred.reject(result);
            });

            return deferred.promise;
        }
        //function sendReceipt(email) {
        //    var deferred = $q.defer();
        //    $http({
        //        url: CustomerUrls.sendReceipt + "?email=" + email,
        //        method: 'GET'
        //    }).success(function (result) {
        //        deferred.resolve(result);
        //    }).error(function () {
        //        deferred.reject();
        //    });
        //    return deferred.promise;
        //}
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
        function sendPaymentNonce(nonce) {
            var deferred = $q.defer();

            $http({
                url: CustomerUrls.postNonce + "?nonce=" + nonce,
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