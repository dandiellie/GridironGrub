(function () {
	'use strict';

	angular
		.module('starter', ['ionic', 'ngCordova'])
		.config(['$stateProvider', '$urlRouterProvider', '$httpProvider', Config])
		.constant('CustomerUrls', {
            token: '/Token',
            register: 'http://localhost:49325/api/Account/Register',
			getProfile: 'http://localhost:49325/api/customer/profile',
			getScan: 'http://localhost:49325/api/customer/scan',
			getWelcome: 'http://localhost:49325/api/customer/welcome',
			getRestaurant: 'http://localhost:49325/api/customer/restaurant',
			getCart: '/api/customer/cart',
			getBadge: '/api/customer/navbarBadge',
			getPaymentToken: '/api/customer/token',
			postProfile: 'http://localhost:49325/api/customer/postProfile',
			postAddItem: '/api/customer/addItem',
			postNonce: '/api/customer/purchase'
        });

	function Config ($stateProvider, $urlRouterProvider, $httpProvider) {

		$stateProvider
			.state('login', {
				url: '/login',
				templateUrl: 'App/Components/Login/loginView.html'
			})
			.state('profile', {
				url: '/profile',
				templateUrl: 'App/Components/Profile/profileView.html'
			}).state('scan', {
				url: '/scan',
				templateUrl: 'App/Components/Scan/scanView.html'
			}).state('order', {
				url: '/order/:seatId',
				templateUrl: 'App/Components/Order/orderView.html'
			}).state('vendors', {
				url: '/vendors/:seatId',
				templateUrl: 'App/Components/Vendors/vendorsView.html'
			})
			.state('item', {
				url: '/item/:restId',
				templateUrl: 'App/Components/Item/itemView.html'
			})
			.state('start', {
				url: '/start',
				templateUrl: 'App/Components/Start/startView.html'
			})
			.state('braintree', {
				url: '/braintree',
				templateUrl: 'App/Components/Braintree/braintreeView.html'
			})
			.state('seat', {
				url: '/seat',
				templateUrl: 'App/Components/Seat/seatView.html'
			})
			.state('otherwise', {url : '/start'});

			$httpProvider.interceptors.push('authService');

			// $urlRouterProvider.otherwise('/start');
	}

})();