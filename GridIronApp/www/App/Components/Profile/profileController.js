( function() {
	'use strict';

	angular
		.module('starter')
		.controller('ProfileController', ['profileService', 'editProfileService', '$location', '$state', '$http', '$scope', ProfileController]);

	function ProfileController(profileService, editProfileService, $location, $state, $http, $scope) {
		$state.reload();

		var vm = this;
		vm.edit = {};
		vm.seeProfile = seeProfile;
		vm.changeProfile = changeProfile;

		function seeProfile() {

		    profileService.getProfile().then(win, lose);

		}

			function win(data) {
	            vm.file = data;
	        }

	        function lose() {
	            console.log('Failed load your profile');
	        }

		vm.seeProfile();

		function changeProfile() {
			$scope.editorEnabled = false;

			$scope.enableEditor = function() {
			  $scope.editorEnabled = true;
			};
			  
			$scope.disableEditor = function() {
			  $scope.editorEnabled = false;
			};
			  
			$scope.save = function() {
				vm.edit.firstName = vm.file.firstName;
				vm.edit.lastName = vm.file.lastName;
				editProfileService.updateProfile(vm.edit).then(success, fail);

				function success(data) {
	        		console.log(data);
	        		profileService.getProfile().then(win, lose);
		        }

		        function fail() {
		        	console.log('Failed to edit Profile');
		        }

				$scope.disableEditor();
			};
		}

		vm.changeProfile();

	}

})();


		