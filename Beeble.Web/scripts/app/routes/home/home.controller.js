angular.module('myApp').controller('homeController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

    var pageNumber = 0;
    $scope.searchQuery = "S";

	$scope.search = function () {
        bookSearchService.search($scope.searchQuery, pageNumber).then(function (response) {
            $scope.currentBooks = response.data;
        });
	}

}]);