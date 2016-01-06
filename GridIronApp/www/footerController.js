(function() {
    'use strict';

    angular
        .module('starter')
        .controller('footerController', ['$location', footerController]);

    function footerController($location) {
        var vm = this;

        vm.goToProfile = goToProfile;
        vm.goToBraintree = goToBraintree;
        vm.goToScan = goToScan;

        function goToProfile()
        {
            $location.path('/profile');
        }
        function goToBraintree()
        {
            $location.path('/braintree');
        }
        function goToScan()
        {
            $location.path('/scan');
        }
    }

})();