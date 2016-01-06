(function () {
    'use strict';

    angular
        .module('starter')
        .controller('profileController', ['profileService', profileController])
        .controller('allOrderController', ['allOrderService', 'acceptOrderService', allOrderController])
        .controller('activeOrderController', ['activeOrderService', 'deliverOrderService', activeOrderController])
        .controller('historyOrderController', ['historyOrderService', historyOrderController]);

    function profileController(profileService) {
        var vm = this;

        profileService.getProfile().then(successGetProfile, failGetProfile);

        function successGetProfile(data) {
            vm.profiles = data;
        }

        function failGetProfile(data) {

        }
    };

    function allOrderController(allOrderService, acceptOrderService) {
        var vm = this;

        vm.takeOrderBtn = takeOrderBtn;

        allOrderService.getAllOrder().then(allOrdersSuccess, allOrdersFail);

        function allOrdersSuccess(data) {
            vm.allOrders = data;
        }

        function allOrdersFail(data) {

        }

        function takeOrderBtn(ordId) {
            acceptOrderService.acceptOrder(ordId).then(acceptOrderSuccess, acceptOrderFail)

            function acceptOrderSuccess() {
                allOrderService.getAllOrder().then(allOrdersSuccess, allOrdersFail);
            }

            function acceptOrderFail() {
                console.log("takeOrderBtn failed");
            }
        }

    };

    function activeOrderController(activeOrderService, deliverOrderService) {
        var vm = this;

        vm.deliverOrderBtn = deliverOrderBtn;

        activeOrderService.getActiveOrder().then(activeOrderSuccess, activeOrderFail)

        function activeOrderSuccess(data) {
            vm.activeOrders = data;
        }

        function activeOrderFail(data) {

        }

        function deliverOrderBtn(ordId) {
            deliverOrderService.deliverOrders(ordId).then(deliverOrderSuccess, deliverOrderFail)

            function deliverOrderSuccess() {
                activeOrderService.getActiveOrder().then(activeOrderSuccess, activeOrderFail)
            }

            function deliverOrderFail() {
                console.log("DeliverOrderBtn failed");
            }
        }

    };

    function historyOrderController(historyOrderService) {
        var vm = this;

        historyOrderService.getHistoryOrder().then(success, fail)

        function success(data) {
            vm.historyOrders = data;
        }

        function fail(data) {

        }
    };

})();