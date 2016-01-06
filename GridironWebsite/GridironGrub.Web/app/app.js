(function () {
    'use strict';

    angular
		.module('starter', ['ngRoute', 'ngAnimate', 'ui.bootstrap', 'angularMoment'])
		.config(['$routeProvider', '$httpProvider', '$locationProvider', Config])
        .directive('ngEnter', ngEnter)
		.constant('AdminUrls', {
		    getOpen: '/api/admin/open',
		    getRecent: '/api/admin/recent',
		    getSpecific: '/api/admin/specific',
		    getRestaurants: '/api/admin/restaurants',
		    getPark: '/api/admin/park',
		    getVendor: '/api/admin/vendor',
            getSeats: '/api/admin/seats',
		    getEmployees: '/api/admin/employees',
            getEmployee: '/api/admin/employee',
            postSeat: '/api/admin/seat',
            postEmployee: '/api/admin/updateEmployee',
            postPark: '/api/admin/park',
            postArea: '/api/admin/area',
            postVendor: '/api/admin/restaurant',
            postCategory: '/api/admin/category',
            postItem: '/api/admin/item',
		}).constant('CustomerUrls', {
		    token: '/Token',
		    register: '/api/Account/Register',
		    getLoginInfo: '/api/login/loginInfo',
		    getScan: '/api/customer/scan',
		    getWelcome: '/api/customer/welcome',
		    getRestaurant: '/api/customer/restaurant',
		    getCart: '/api/customer/cart',
		    getBadge: '/api/customer/navbarBadge',
		    containsAlcohol: '/api/customer/alcohol',
		    sendReceipt: '/api/customer/receipt',
		    getPaymentToken: '/api/customer/token',
		    postProfile: '/api/customer/postProfile',
		    postAddItem: '/api/customer/addItem',
		    postRetireOrder: '/api/customer/retireOrder',
		    postNonce: '/api/customer/purchase'
		}).constant('ManagerUrls', {
		    getVendors: '/api/manager/vendors',
		    getVendor: '/api/manager/vendor',
		    postVendor: '/api/manager/vendor',
		    postCategory: '/api/manager/category',
		    postItem: '/api/manager/item',
		}).constant('RunnerUrls', {
		    getProfile: '/api/runner/profile',
		    getAllOrders: '/api/runner/open',
		    getActiveOrders: '/api/runner/active',
		    getHistoryOrders: '/api/runner/history',
		    postAcceptOrders: '/api/runner/accept',
            postDeliverOrders: '/api/runner/deliver'
		}).constant('UserUrls', {
		    getInfoIds: '/api/login/infoIds',
            getRoles: '/api/login/roles',
            getForgotPassword: '/api/login/forgotPassword',
            postReset: '/api/login/resetPassword'
		}).constant('VendorUrls', {
		    getOpen: '/api/vendor/open',
		    getRecent: '/api/vendor/recent',
		    postComplete: '/api/vendor/complete'
		});

    function Config($routeProvider, $httpProvider, $locationProvider) {
        $httpProvider.interceptors.push('authService');
        $locationProvider.html5Mode(true);

        $routeProvider
            .when('/', { // home page
                templateUrl: 'app/components/index.html',
                controller: 'loginController',
                controllerAs: 'vm'
            }).when('/bulletin', { // bulletin page
                templateUrl: 'app/components/bulletin.html',
                controller: 'loginController',
                controllerAs: 'vm'
            }).when('/login', { // login page
                templateUrl: 'app/components/login/loginView.html',
                controller: 'loginController',
                controllerAs: 'vm'
            }).when('/password', { // forgot password page
                templateUrl: 'app/components/password/passwordView.html',
                controller: 'passwordController',
                controllerAs: 'vm'
            }).when('/password/:user', { // reset password page
                templateUrl: 'app/components/password/passwordView.html',
                controller: 'passwordController',
                controllerAs: 'vm'
            }).when('/orders', { // admin pages
                templateUrl: 'app/components/admin/orders/ordersView.html',
                controller: 'ordersController',
                controllerAs: 'vm'
            }).when('/employees', {
                templateUrl: 'app/components/admin/employees/employeesView.html',
                controller: 'employeesController',
                controllerAs: 'vm'
            }).when('/park', {
                templateUrl: 'app/components/admin/park/parkView.html',
                controller: 'parkController',
                controllerAs: 'vm'
            }).when('/shop', { // customer pages
                templateUrl: 'app/components/customer/shop/shopView.html',
                controller: 'shopController',
                controllerAs: 'vm'
            }).when('/shop/:seatId', {
                templateUrl: 'app/components/customer/shop/shopView.html',
                controller: 'shopController',
                controllerAs: 'vm'
            }).when('/order/:restId', {
                templateUrl: 'app/components/customer/order/orderView.html',
                controller: 'orderController',
                controllerAs: 'vm'
            }).when('/manager', { // manager page
                templateUrl: 'app/components/manager/managerView.html',
                controller: 'managerController',
                controllerAs: 'vm'
            }).when('/runner', {
                templateUrl: 'app/components/runner/views/profileView.html',
                controller: 'profileController',
                controllerAs: 'vm'
            }).when('/historyOrder', {
                templateUrl: 'app/components/runner/views/historyOrderView.html',
                controller: 'historyOrderController',
                controllerAs: 'vm'
            }).when('/activeOrder', {
                templateUrl: 'app/components/runner/views/activeOrderView.html',
                controller: 'activeOrderController',
                controllerAs: 'vm'
            }).when('/allOrder', {
                templateUrl: 'app/components/runner/views/allOrderView.html',
                controller: 'allOrderController',
                controllerAs: 'vm'
            }).when('/vendor', { // vendor page
                templateUrl: 'app/components/vendor/vendorView.html',
                controller: 'vendorController',
                controllerAs: 'vm'
            });
    }

    function ngEnter() {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });
                    event.preventDefault();
                }
            })
        }
    }

})();