(function () {

    angular
        .module('starter')
        .controller('vendorController', ['vendorService', vendorController]);

    function vendorController(vendorService) {
        var vm = this;
        vm.showOpen = true;
        vm.showRecent = false;

        vm.goToOpen = goToOpen;
        vm.goToRecent = goToRecent;
        vm.complete = complete;

        //setInterval(vendorService.getOpen().then(successGetOpen, failGetOpen), 5000);
        vendorService.getOpen().then(successGetOpen, failGetOpen);

        function goToOpen() {
            vm.showOpen = true;
            vm.showRecent = false;
            vendorService.getOpen().then(successGetOpen, failGetOpen);
        }
        function goToRecent() {
            vm.showOpen = false;
            vm.showRecent = true;
            vendorService.getRecent().then(successGetRecent, failGetRecent);
        }
        function complete(orderId) {
            vendorService.postComplete(orderId).then(successComplete, failComplete);
        }

        function successGetOpen(data) {
            vm.open = data;
        }
        function failGetOpen(data) {

        }

        function successGetRecent(data) {
            vm.recent = data;
        }
        function failGetRecent(data) {

        }

        function successComplete(data) {
            goToOpen();
        }
        function failComplete(data) {

        }

    }
})();