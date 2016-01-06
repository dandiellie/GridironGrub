(function () {
    angular
		.module('starter')
		.service('employeesService', ['$q', '$http', 'AdminUrls', employeesService])

    function employeesService($q, $http, AdminUrls) {
        var service = {};

        service.getRestaurants = getRestaurants;
        service.getEmployees = getEmployees;
        service.getEmployee = getEmployee;
        service.postEmployee = postEmployee;

        function getRestaurants() {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getRestaurants,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getEmployees() {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getEmployees,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function getEmployee(username) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.getEmployee + "?username=" + username,
                method: 'GET'
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function () {
                deferred.reject();
            });

            return deferred.promise;
        }
        function postEmployee(vm) {
            var deferred = $q.defer();

            $http({
                url: AdminUrls.postEmployee,
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