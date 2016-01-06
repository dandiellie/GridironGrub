( function() {
	angular
		.module('starter')
		.controller('RegisterController', ['registerService', '$location', '$scope', '$ionicPopup', RegisterController]);

	    function RegisterController(registerService, $location, $scope, $ionicPopup) {
        var vm = this;
        vm.registerModel = {};
        vm.isLoading = false;
        vm.register = register;

        function register() {
            vm.isLoading = !vm.isLoading;
            vm.registerModel.email = vm.email;
            vm.registerModel.password = vm.password;
            vm.registerModel.confirmPassword = vm.confirmPassword;
            vm.registerModel.birthday = vm.birthday;
            registerService.register(vm.registerModel).then(success, fail);
        }

        function success() {
            vm.isLoading = !vm.isLoading;
            var alertSuccessPopup = $ionicPopup.alert({
                title: 'Register Successful',
                template: 'Now please sign in above!'
            });
            vm.email = '';
            vm.password = '';
            vm.confirmPassword = '';
            vm.birthday = '';
            $location.path('/login');
        }

        function fail(data) {
            vm.isLoading = !vm.isLoading;
            var alertFailPopup = $ionicPopup.alert({
                title: 'Register Failed',
                template: 'Please check your inputs!'
            });
        }
    }
})();