var app = angular.module('myApp').controller("getMembershipController", function ($scope, $state, getLibrariesService, $stateParams) {

	$scope.search = function () {
		if ($scope.searchQuery)
			$state.go('search', { searchQuery: $scope.searchQuery });
	};

	getLibrariesService.getLibraryByIdForMembership($stateParams.libraryId).then(function(response) {
		$scope.library = response.data;
		console.log(response.data);

	});

});