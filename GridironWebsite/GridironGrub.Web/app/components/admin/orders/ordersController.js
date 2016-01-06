(function () {

    angular
        .module('starter')
        .controller('ordersController', ['ordersService', ordersController]);

    function ordersController(ordersService) {
        var vm = this;
        vm.showOpen = true;
        vm.showRecent = false;
        vm.showSpecific = false;
        vm.sortType = 'time'; // set the default sort type
        vm.sortReverse = false;  // set the default sort order
        vm.search = '';     // set the default search/filter term

        vm.goToOpen = goToOpen;
        vm.goToRecent = goToRecent;
        vm.goToSpecific = goToSpecific;
        vm.getOpen = getOpen;
        vm.getRecent = getRecent;
        vm.getSpecific = getSpecific;

        ordersService.getOpen().then(successOpen, failOpen);

        function goToOpen() {
            vm.showOpen = true;
            vm.showRecent = false;
            vm.showSpecific = false;
            vm.getOpen();
        }
        function goToRecent() {
            vm.showOpen = false;
            vm.showRecent = true;
            vm.showSpecific = false;
            vm.getRecent(null);
        }
        function goToSpecific() {
            vm.showOpen = false;
            vm.showRecent = false;
            vm.showSpecific = true;
        }
        function getOpen() {
            ordersService.getOpen().then(successOpen, failOpen);
        }
        function getRecent(date) {
            ordersService.getRecent(date).then(successRecent, failRecent);
        }
        function getSpecific(id) {
            ordersService.getSpecific(id).then(successSpecific, failSpecific);
        }

        function successOpen(data) {
            vm.open = data;
        }
        function failOpen(data) {

        }

        function successRecent(data) {
            vm.recent = data;
        }
        function failRecent(data) {

        }

        function successSpecific(data) {
            vm.specific = data;
        }
        function failSpecific(data) {

        }
    }
})();