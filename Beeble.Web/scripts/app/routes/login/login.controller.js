angular.module('myApp').controller('loginController', ['$scope', '$state', '$location', 'authService', function ($scope, $state, $location, authService) {

    $scope.loginData = {
        userName: "jsvalina",
        password: "123456"
    };

    $scope.teset = "dsadas";

    $scope.login = function () {
        console.log('loggin');
        authService.login($scope.loginData).then(function (response) {

                //$location.path('/orders');
                console.log('login successful');

            },
            function (err) {
                console.log(err.error_description);
            });
    };

}]);