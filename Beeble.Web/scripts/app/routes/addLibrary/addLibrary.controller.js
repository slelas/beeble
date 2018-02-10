angular.module('myApp').controller('addLibraryController',
	function ($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, getLibrariesService) {

		var allLibraries = [];
		var popupActive, paymentActive = false;

        $scope.togglePopup = function (library) {
            $scope.library = library;
			$scope.popupActive = !$scope.popupActive;
            $scope.paymentActive = false;

            if ($scope.popupActive) {
                // Paypal information
                $scope.paymentName =
                    'Library: ' + $scope.library.name +
                    ' (ID: ' + $scope.library.id +
                    ') - MEMBERSHIP';
                $scope.price = $scope.library.membershipPrice;
            }
        }

        $scope.submitBarcode = function (library) {
            getLibrariesService.submitBarcode(library.id, $scope.barcodeNumber).then(function (response) {
            });
        };

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