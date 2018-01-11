angular.module('myApp').controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    // currently unused
    $scope.savedSuccessfully = false;

	$scope.registration = {
		username: "test@test.hr",
        password: "123456",
		confirmPassword: "123456",
		name: "test ime",
		lastname: "test prezime",
		oib: "123456789",
		address: "test adresa",
		city: "test grad",
		phoneNumber: "123456"
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

                $scope.savedSuccessfully = true;
                console.log('signup successful');

            },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
                console.log('Failed to register user due to:' + errors.join(' '));
            });
    };
}]);