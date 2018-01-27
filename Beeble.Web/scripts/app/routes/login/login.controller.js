angular.module('myApp').controller('loginController', ['$scope', '$state', '$location', 'authService', function ($scope, $state, $location, authService) {

    $scope.loginData = {
        userName: "jsvalina",
        password: "123456"
    };

	$scope.login = function () {
		authService.login($scope.loginData, $scope.isUserRemembered);
	};

}]);