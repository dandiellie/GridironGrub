(function () {
    'use strict';

    angular
        .module('starter')
        .controller('loginController', ['loginService', '$location', loginController]);

    function loginController(loginService, $location) {
        var vm = this;
        vm.isLoading = false;

        vm.login = login;
        vm.isLoggedIn = isLoggedIn;
        vm.isAdmin = isAdmin;
        vm.isManager = isManager;
        vm.isVendor = isVendor;
        vm.isRunner = isRunner;
        vm.isSeat = isSeat;
        vm.isCustomer = isCustomer;
        vm.logout = logout;

        function login() {
            vm.isLoading = true;
            loginService.login(vm.username, vm.password).then(successLogin, failLogin);
        }
        function isLoggedIn() {
            return loginService.isLoggedIn();
        }
        function isAdmin() {
            return loginService.isAdmin();
        }
        function isManager() {
            return loginService.isManager();
        }
        function isVendor() {
            return loginService.isVendor();
        }
        function isRunner() {
            return loginService.isRunner();
        }
        function isSeat() {
            return loginService.isSeat();
        }
        function isCustomer() {
            return loginService.isCustomer();
        }
        function logout() {
            loginService.logout();
            $location.path('/');
        }

        function successLogin(data) {
            //loginService.getRoles().then(successGetRoles, failGetRoles);
            $location.path('/bulletin');
        }
        function failLogin(data) {
            vm.isLoading = false;
        }

        function successGetRoles(data) {
            vm.isLoading = false;
            vm.roles = data;
        }
        function failGetRoles(data) {
            vm.isLoading = false;
        }

        function successGetInfoIds(data) {
            vm.isLoading = false;
            vm.isAdmin = data.AdminInfoId;
            vm.isManager = data.ManagerInfoId;
            vm.isVendor = data.VendorInfoId;
            vm.isRunner = data.RunnerInfoId;
            vm.isCustomer = data.CustInfoId;
            vm.isSeat = data.SeatInfoId;
            $location.path('/shop');
        }
        function failGetInfoIds(data) {
            vm.isLoading = false;
            console.log("Failed in controller.");
        }
    }
})();