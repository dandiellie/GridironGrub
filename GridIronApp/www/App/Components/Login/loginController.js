( function() {
	'use strict';

    angular
        .module('starter')
        .controller('LoginController', ['loginService', '$location', LoginController]);
        // .controller('HeaderController', ['loginService', '$location', HeaderController])


    function LoginController(loginService, $location) {
        var vm = this;

        vm.login = login;
        // vm.isLoading = isLoading;
        // vm.loading = false;

        // function isLoading() {
        //     return vm.loading;
        // }

        function login() {
            // vm.loading = true;
            loginService.login(vm.username, vm.password).then(success, fail);
        }

        function success() {
            // vm.loading = false;
            $location.path('/profile');
        }

        function fail() {
            // vm.loading = false;
            alert('Login failed!');
        }
    }

    //  function HeaderController(loginService, $location) {
    //     var vm = this;

    //     vm.isLoggedIn = isLoggedIn;
    //     vm.logout = logout;

    //     function isLoggedIn() {
    //         return loginService.isLoggedIn();
    //     }

    //     function logout() {
    //         loginService.logout();
    //         $location.path('/');
    //     }
    // }

})();