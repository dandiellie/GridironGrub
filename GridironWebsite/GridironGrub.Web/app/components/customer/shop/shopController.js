(function () {

    angular
        .module('starter')
        .controller('shopController', ['shopService', 'loginService', 'orderService', '$location', '$routeParams', shopController]);

    function shopController(shopService, loginService, orderService, $location, $routeParams) {
        var vm = this;
        vm.isLoading = false;
        vm.showScan = true;
        vm.showLoading = false;
        vm.showWelcome = false;
        vm.showOutArea = false;
        vm.seatId = null;
        vm.seatErrorMessage = '';
        vm.itemToAdd = {};

        vm.goToScan = goToScan;
        vm.goToLoading = goToLoading;
        vm.goToWelcome = goToWelcome;
        vm.goToOutArea = goToOutArea;
        vm.goToItem = goToItem;
        vm.getLoginInfo = getLoginInfo;

        var searchTerm = $routeParams.seatId;
        if (searchTerm) {
            vm.seatId = searchTerm;
            vm.goToWelcome();
        }

        function goToScan() {
            vm.showScan = true;
            vm.showLoading = false;
            vm.showWelcome = false;
            vm.showOutArea = false;
        }
        function goToLoading() {
            vm.showScan = false;
            vm.showLoading = true;
            vm.showWelcome = false;
            vm.showOutArea = false;
            vm.getLoginInfo();
        }
        function goToWelcome() {
            if (vm.seatId) {
                vm.showScan = false;
                vm.showLoading = false;
                vm.showWelcome = true;
                vm.showOutArea = false;
                shopService.getWelcome(vm.seatId).then(successGetWelcome, failGetWelcome);
                shopService.getRestaurantsFromScan(vm.seatId).then(successGetRestaurants, failGetRestaurants);
                shopService.retireOrder().then(successRetireOrder, failRetireOrder);
            }
            else {
                vm.seatErrorMessage = "ReEnter your Seat Id #"
                vm.goToScan();
            }
        }
        function goToOutArea() {
            vm.showScan = false;
            vm.showLoading = false;
            vm.showWelcome = false;
            vm.showOutArea = true;
        }
        function goToItem(restId) {
            $location.path('/order/' + restId);
        }
        function getLoginInfo() {
            vm.isLoading = !vm.isLoading;
            shopService.getLoginInfo(vm.seatId).then(successLoginInfo, failLoginInfo);
        }

        function successGetWelcome(data) {
            vm.isLoading = !vm.isLoading;
            vm.welcome = data;
        }
        function failGetWelcome(data) {
            vm.isLoading = !vm.isLoading;
        }

        function successGetRestaurants(data) {
            vm.rests = data;
        }
        function failGetRestaurants(data) {
            
        }

        function successLoginInfo(data) {
            loginService.login(data.username, data.password).then(successLogin, failLogin);
        }
        function failLoginInfo(data) {

        }

        function successLogin(data) {
            goToWelcome();
        }
        function failLogin(data) {

        }

        function successRetireOrder(data) {

        }
        function failRetireOrder(data) {

        }

    }

})();