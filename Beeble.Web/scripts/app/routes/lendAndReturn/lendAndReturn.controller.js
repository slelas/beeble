angular.module('myApp').controller('lendAndReturnController', ['$scope', '$state', '$location', 'authService', function ($scope, $state, $location, authService) {

    $scope.test = function () {
        console.log('test');
    };

    $scope.var = function () {
        console.log(typeof ($scope.test));
    };

    $scope.var();
}]);