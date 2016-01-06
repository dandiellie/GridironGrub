(function () {

    angular
        .module('starter')
        .controller('orderController', ['$window', '$routeParams', 'orderService', 'shopService', 'loginService', '$location', orderController]);

    function orderController($window, $routeParams, orderService, shopService, loginService, $location) {
        var vm = this;
        vm.isLoading = false;
        vm.showItem = true;
        vm.showCart = false;
        vm.showWarning = false;
        vm.showDescription = false;
        vm.showPayment = false;
        vm.showConfirmation = false;
        vm.restId = 0;
        vm.itemToAdd = {};
        vm.description = {};
        var searchTerm = $routeParams.restId;
        if (searchTerm) {
            vm.restId = searchTerm;
            shopService.getRestaurant(vm.restId).then(successGetRestaurant, failGetRestaurant);
        }

        vm.goToItem = goToItem;
        vm.addToOrder = addToOrder;
        vm.goToCart = goToCart;
        vm.goToWarning = goToWarning;
        vm.goToDescription = goToDescription;
        vm.goToPayment = goToPayment;
        vm.goToConfirmation = goToConfirmation;
        vm.goToWelcome = goToWelcome;
        vm.plus = plus;
        vm.minus = minus;
        vm.showAlert = showAlert;
        vm.saveDescription = saveDescription;

        function goToItem() {
            vm.showItem = true;
            vm.showCart = false;
            vm.showWarning = false;
            vm.showDescription = false;
            vm.showPayment = false;
            vm.showConfirmation = false;
            shopService.getRestaurant(vm.restId).then(successGetRestaurant, failGetRestaurant);
            orderService.getNumItems().then(successGetNumItems, failGetNumItems);
        }
        function addToOrder(itemId) {
            vm.itemToAdd.itemId = itemId;
            shopService.addItem(vm.itemToAdd).then(successAdd, failAdd);
        }
        function goToCart() {
            vm.showItem = false;
            vm.showCart = true;
            vm.showWarning = false;
            vm.showDescription = false;
            vm.showPayment = false;
            vm.showConfirmation = false;
            orderService.getCart().then(successGetCart, failGetCart);
        }
        function goToWarning() {
            vm.showItem = false;
            vm.showCart = false;
            vm.showWarning = true;
            vm.showDescription = false;
            vm.showPayment = false;
            vm.showConfirmation = false;
        }
        function goToDescription() {
            vm.showItem = false;
            vm.showCart = false;
            vm.showWarning = false;
            vm.showDescription = true;
            vm.showPayment = false;
            vm.showConfirmation = false;
        }
        function goToPayment() {
            vm.showItem = false;
            vm.showCart = false;
            vm.showWarning = false;
            vm.showDescription = false;
            vm.showPayment = true;
            vm.showConfirmation = false;
        }
        function goToConfirmation() {
            vm.showItem = false;
            vm.showCart = false;
            vm.showWarning = false;
            vm.showDescription = false;
            vm.showPayment = false;
            vm.showConfirmation = true;
        }
        function goToWelcome() {
            shopService.retireOrder().then(successRetireOrder, failRetireOrder);
            $location.path('/shop/' + vm.items.seatId);
        }
        function plus(itemId) {
            vm.itemToAdd.itemId = itemId;
            shopService.addItem(vm.itemToAdd).then(successPlus, failPlus);
        }
        function minus(itemId, itemQuantity) {
            vm.itemToAdd.itemId = itemId;
            if (itemQuantity > 1) {
                vm.itemToAdd.quantity = itemQuantity - 1;
            }
            else {
                vm.itemToAdd.removeItemId = itemId;
            }
            shopService.addItem(vm.itemToAdd).then(successPlus, failPlus);
        }
        function showAlert() {
            orderService.containsAlcohol().then(successAlcohol, failAlcohol);
        }
        function saveDescription() {
            shopService.addItem(vm.description).then(successSaveDescription, failSaveDescription);
        }

        function successGetRestaurant(data) {
            vm.items = data;
            orderService.getPaymentToken().then(successGetToken, failGetToken);
            console.log(data);
        }
        function failGetRestaurant(data) {

        }

        function successAdd(data) {
            orderService.getNumItems().then(successGetNumItems, failGetNumItems);
        }
        function failAdd(data) {

        }

        function successGetNumItems(data) {
            vm.numItems = data;
        }
        function failGetNumItems(data) {

        }

        function successGetCart(data) {
            vm.cart = data;
        }
        function failGetCart(data) {

        }

        function successPlus(data) {
            orderService.getCart().then(successGetCart, failGetCart);
            vm.itemToAdd = {};
        }
        function failPlus(data) {

        }

        function successMinus(data) {
            orderService.getCart().then(successGetCart, failGetCart);
            vm.itemToAdd = {};
        }
        function failMinus(data) {
            vm.itemToAdd = {};
        }

        function successAlcohol(data) {
            if (data) {
                vm.goToWarning();
            }
            else {
                vm.goToDescription();
            }
        }
        function failAlcohol(data) {

        }

        //function successReceipt(data) {
        //    shopService.addItem(vm.description).then(successSaveDescription, failSaveDescription);
        //}
        //function failReceipt(data) {
        //}

        function successSaveDescription(data) {
            vm.goToPayment();
            //if (vm.description.receiptEmail) {
            //    orderService.sendReceipt(vm.description.receiptEmail).then(successReceipt, failReceipt);
            //}
        }
        function failSaveDescription(data) {

        }

        function successGetToken(data) {
            var token = $window.sessionStorage.getItem('token');
            braintree.setup(data, "dropin", {
                container: "payment-form",
                paymentMethodNonceReceived: function (event, nonce) {
                    vm.isLoading = true;
                    goToConfirmation();
                    orderService.sendPaymentNonce(nonce).then(successSendNonce, failSendNonce);
                }
            });
        }
        function failGetToken(data) {
            console.log("fail get token");
        }

        function successSendNonce(data) {
            loginService.logout();
        }
        function failSendNonce(data) {
            
        }

        function successRetireOrder(data) {

        }
        function failRetireOrder(data) {

        }
    }

})();