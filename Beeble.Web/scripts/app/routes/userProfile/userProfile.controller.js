angular.module('myApp').controller('userProfileController', ['$scope', '$state', '$location', 'authService', 'userService', function ($scope, $state, $location, authService, userService) {
	userService.getUser().then(function (response) {
		$scope.user = response.data;
		console.log(response.data);
	});

	$scope.save = function () {

		if (($scope.user.email && $scope.user.name && $scope.user.lastname && $scope.user.oib && $scope.user.address && $scope.user.city && $scope.user.phoneNumber) || $scope.user.password !== $scope.user.confirmPassword || ($scope.user.password && $scope.user.password.length < 6))
		{
			alert('nedobri podatci');
			return null;
		}

		$scope.user.userName = $scope.user.email;

		userService.editUser($scope.user)
			.then(function(result) {
				console.log(result);
			});
	}
}]);