( function() {

    angular
        .module('starter')
        .controller('orderController', ['orderService', '$stateParams', '$location', orderController]);

    function orderController(orderService, $stateParams, $location) {
        var vm = this;
        vm.seatId = $stateParams.seatId;

        vm.getRestaurants = getRestaurants;

        if(vm.seatId) {
            orderService.getWelcome(vm.seatId).then(successGetWelcome, failGetWelcome);
        }

        function getRestaurants() {
            $location.path('/vendors/' + vm.seatId);
        }

        function successGetWelcome(data) {
            console.log(data);
            vm.welcome = data;
        }
        function failGetWelcome(data) {
            console.log(data);
        }
    }

})();