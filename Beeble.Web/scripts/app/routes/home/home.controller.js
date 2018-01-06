angular.module('myApp').controller('homeController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

	var pageNumber = 0;

	$scope.search = function () {
		$scope.currentBooks = bookSearchService.search($scope.searchQuery, pageNumber);
	}

}]);