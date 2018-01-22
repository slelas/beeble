angular.module('myApp').controller('addLibraryController',
	function ($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, getLibrariesService) {

		var allLibraries = [];
		var popupActive = false, paymentActive = false;

		$scope.togglePopup = function() {
			$scope.popupActive = !$scope.popupActive;
		}

		$scope.togglePayment = function() {
			$scope.paymentActive = !$scope.paymentActive;
		}

		getLibrariesService.getAllLibraries().then(function(response) {
			allLibraries = response.data;
			$scope.allSearchLibraries = response.data;
		});

		$scope.$watch('searchQuery',
			function(searchQuery) {
				$scope.allSearchLibraries =
					allLibraries.filter(library => library.name.toLowerCase().indexOf(searchQuery.toLowerCase()) !== -1);
			});

});