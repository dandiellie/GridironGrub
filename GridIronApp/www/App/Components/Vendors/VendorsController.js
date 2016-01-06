(function() {

    angular
        .module('starter')
        .controller('vendorsController', ['vendorsService', '$stateParams', '$location', vendorsController]);

    function vendorsController(vendorsService, $stateParams, $location) {
        var vm = this;
        vm.seatId = $stateParams.seatId;

        vm.goToItem = goToItem;

        if(vm.seatId) {
            vendorsService.getRestaurantsFromScan(vm.seatId).then(successGetRestaurants, failGetRestaurants);
        }

        function goToItem(restId) {
            $location.path('/item/' + restId);
        }

        function successGetRestaurants(data) {
            console.log(data);
            vm.rests = data;
        }
        function failGetRestaurants(data) {
            console.log(data);
        }
    }

})();