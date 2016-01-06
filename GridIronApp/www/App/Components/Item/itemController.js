(function() {

    angular
        .module('starter')
        .controller('itemController', ['itemService', '$stateParams', '$location', itemController]);

    function itemController(itemService, $stateParams, $location) {
        var vm = this;
        vm.restId = $stateParams.restId;

        vm.toggleGroup = toggleGroup;
        vm.isGroupShown = isGroupShown;

        if(vm.restId) {
            itemService.getRestaurant(vm.restId).then(successGetRestaurant, failGetRestaurant);
        }

        function toggleGroup(group) {
            if (vm.isGroupShown(group)) {
                vm.shownGroup = null;
            } else {
                vm.shownGroup = group;
            }
        };

        function isGroupShown(group) {
            return vm.shownGroup === group;
        };

        function successGetRestaurant(data) {
            console.log(data);
            vm.items = data;
        }
        function failGetRestaurant(data) {
            console.log(data);
        }
    }

})();