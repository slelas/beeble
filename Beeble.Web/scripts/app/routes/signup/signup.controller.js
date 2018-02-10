angular.module('myApp').controller('signupController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.savedSuccessfully = false;

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {
                $scope.savedSuccessfully = true;

            },
            function (response) {
                var errors = [];
                for (var key in response.data.modelState) {
                    for (var i = 0; i < response.data.modelState[key].length; i++) {
                        errors.push(response.data.modelState[key][i]);
                    }
                }
            });
    };
}]);