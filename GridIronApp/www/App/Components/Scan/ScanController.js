( function() {

    angular
        .module('starter')
        .controller('scanController', ['$cordovaBarcodeScanner', '$scope', '$window', '$location', scanController]);

    function scanController($cordovaBarcodeScanner, $scope, $window, $location) {
        
        $scope.scanBarcode = scanBarcode;
        $scope.getRestaurants = getRestaurants;

        function scanBarcode()
        {
            cordova.plugins.barcodeScanner.scan(successScan, failScan);
        }

        function getRestaurants(seatId)
        {
            $location.path('/order/' + seatId);
        }

        function successScan(data)
        {
            $location.path('/order/' + data);
            console.log("Barcode Format -> " + data.format);
            console.log("Cancelled -> " + data.cancelled);
        }
        function failScan(data)
        {
            console.log("An error happened -> " + data);
        }
    }

})();