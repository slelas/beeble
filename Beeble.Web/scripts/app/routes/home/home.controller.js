var app = angular.module('myApp').controller("homeController", function ($scope, $state) {

	$scope.search = function () {
		if ($scope.searchQuery)
			$state.go('search', { searchQuery: $scope.searchQuery });
	};
});