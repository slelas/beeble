var app = angular.module('myApp').controller("getMembershipController", function ($scope, $state, getLibrariesService, $stateParams) {

	$scope.search = function () {
		if ($scope.searchQuery)
			$state.go('search', { searchQuery: $scope.searchQuery });
	};

	getLibrariesService.getLibraryByIdForMembership($stateParams.libraryId).then(function(response) {
        $scope.library = response.data;

        // Paypal information
        $scope.paymentName =
            'Library: ' + $scope.library.name +
            ' (ID: ' + $scope.library.id +
            ') - MEMBERSHIP';
        $scope.price = $scope.library.membershipPrice;

    });

    $scope.submitBarcode = function () {
        getLibrariesService.submitBarcode($stateParams.libraryId, $scope.barcodeNumber).then(function (response) {
        });
    };

});