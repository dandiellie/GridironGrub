( function() {

    angular
        .module('starter')
        .controller('braintreeController', ['braintreeService', braintreeController]);

    function braintreeController(braintreeService) {
        var vm = this;
        
        vm.submitToBraintree = submitToBraintree;

        braintreeService.getPaymentToken().then(successGetToken, failGetToken);

        function submitToBraintree()
        {
            braintree.onSubmitEncryptForm('checkout', function(e) {
                e.preventDefault();
                var form = $('#checkout')
                if (!$(form).valid()) {
                    return false;
                }
                var options = {
                    beforeSend: showProcessing,
                    error: subscribeErrorHandler, 
                    success: subscribeResponseHandler, 
                    complete:  hideProcessing,
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    dataType: 'json'
                };
                $(form).ajaxSubmit(options);
                return false;
            });
        }

        function successGetToken(data)
        {
            braintree.setup(data, "dropin", {
                container: "payment-form"
            });
        }
        function failGetToken(data)
        {
            console.log("fail get token");
        }
    }

})();