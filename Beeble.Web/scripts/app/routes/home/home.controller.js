var app = angular.module('myApp').controller("homeController", function ($scope, $state, authService) {

	$scope.search = function () {
		if ($scope.searchQuery) {
			$state.go('search', { searchQuery: $scope.searchQuery });

		}
    };

    $scope.isLoggedIn = authService.authentication.isAuth;

    $scope.logOut = function () {
        authService.logOut();
        $scope.isLoggedIn = false;
    }
});