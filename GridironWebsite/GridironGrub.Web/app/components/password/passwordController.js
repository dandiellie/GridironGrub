(function () {
    'use strict';

    angular
        .module('starter')
        .controller('passwordController', ['passwordService', '$location', '$routeParams', passwordController]);

    function passwordController(passwordService, $location, $routeParams) {
        var vm = this;
        vm.showForgot = true;
        vm.showConfirmation = false;
        vm.showReset = false;
        vm.isLoading = false;
        vm.reset = {};

        vm.goToForgot = goToForgot;
        vm.goToConfirmation = goToConfirmation;
        vm.goToReset = goToReset;
        vm.sendEmail = sendEmail;
        vm.resetPassword = resetPassword;

        var searchTerm = $routeParams.user;
        if (searchTerm) {
            vm.reset.userId = searchTerm;
            vm.goToReset();
        }

        function goToForgot() {
            vm.showForgot = true;
            vm.showConfirmation = false;
            vm.showReset = false;
        }
        function goToConfirmation() {
            vm.showForgot = false;
            vm.showConfirmation = true;
            vm.showReset = false;
        }
        function goToReset() {
            vm.showForgot = false;
            vm.showConfirmation = false;
            vm.showReset = true;
        }
        function sendEmail() {
            vm.isLoading = true;
            passwordService.sendEmail(vm.email).then(successSendEmail, failSendEmail);
            vm.goToConfirmation();
        }
        function resetPassword() {
            vm.isLoading = true;
            passwordService.resetPassword(vm.reset).then(successReset, failReset);
        }

        function successSendEmail(data) {
            vm.isLoading = false;
        }
        function failSendEmail(data) {
            vm.isLoading = false;
        }

        function successReset(data) {
            vm.isLoading = false;
            $location.path('/login');
        }
        function failReset(data) {
            vm.isLoading = false;
        }
    }
})();