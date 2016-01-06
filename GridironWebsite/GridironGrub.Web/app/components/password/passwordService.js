(function () {
    'use strict';

    angular
        .module('starter')
        .factory('passwordService', ['$http', '$q', '$window', 'UserUrls', passwordService]);

    function passwordService($http, $q, $window, UserUrls) {
        var service = {};

        service.sendEmail = sendEmail;
        service.resetPassword = resetPassword;

        function sendEmail(email) {
            var deferred = $q.defer();

            $http({
                url: UserUrls.getForgotPassword + "?email=" + email,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function resetPassword(vm) {
            var deferred = $q.defer();

            $http({
                url: UserUrls.postReset,
                method: 'POST',
                data: vm
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