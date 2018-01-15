angular.module('myApp').controller('loginController', ['$scope', '$state', '$location', 'authService', function ($scope, $state, $location, authService) {

    $scope.loginData = {
        userName: "zdelas",
        password: "123456"
    };

	$scope.login = function () {
		console.log($scope.isUserRemembered);
		authService.login($scope.loginData, $scope.isUserRemembered);
	};

}]);