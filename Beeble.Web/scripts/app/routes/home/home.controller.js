angular.module('myApp').controller('homeController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

    var pageNumber = 0;
	$scope.searchQuery = "S";
	var filterStatusList = new Array();

	$scope.search = function () {

		bookSearchService.search($scope.searchQuery, pageNumber).then(function(response) {
			$scope.currentBooks = response.data;
		});

		bookSearchService.getFilters($scope.searchQuery).then(function (response) {

		// response.data is an array of two arrays
			$scope.searchFilters = response.data;
			console.log($scope.searchFilters);

			$scope.searchFilters.forEach(function() {
				filterStatusList.push(true);
			});

			console.log(filterStatusList);

		});

	}

	// called when a user unticks a filter
	$scope.changeFilterStatus = function(index) {
		filterStatusList[index] = !filterStatusList[index];
		console.log(filterStatusList);
	}

	$scope.applyFilters = function() {

		var selectedFilters = $scope.searchFilters[0].filter(function (item, index) {
			return filterStatusList[index];
		});

		bookSearchService.search($scope.searchQuery, pageNumber, selectedFilters).then(function (response) {
			$scope.currentBooks = response.data;
		});

		console.log(selectedFilters);
	}

}]);