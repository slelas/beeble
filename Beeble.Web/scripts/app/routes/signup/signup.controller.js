angular.module('myApp').controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    // currently unused
    $scope.savedSuccessfully = false;

    $scope.registration = {
        userName: "",
        password: "",
		confirmPassword: "",
		name: "",
		lastName: "",
		oib: "",
		address: "",
		city: "",
		phoneNumber: ""
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