var app = angular.module('myApp').controller("headerController", function ($scope, $state, authService) {

    $scope.isLoggedIn = authService.authentication.isAuth;

    $scope.logOut = function () {
        authService.logOut();
        $scope.isLoggedIn = false;
        $state.go('home');
    }
});