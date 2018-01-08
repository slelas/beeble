angular.module('myApp').controller('searchController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

    var pageNumber = 0;
    $scope.searchQuery = "A";
    var filterStatusList = new Array();
    var preloadedResults = new Array();
    $scope.noMoreSearchResults = false

	$scope.search = function () {

		pageNumber = 0;
		bookSearchService.search($scope.searchQuery, pageNumber).then(function(response) {
			$scope.currentBooks = response.data;
			console.log($scope.currentBooks);

		});

		$scope.searchFilters = [];
		bookSearchService.getFilters($scope.searchQuery).then(function (response) {

			filterStatusList = [];

		// response.data is an array of two arrays
			$scope.searchFilters = response.data;
			//console.log($scope.searchFilters);

			$scope.searchFilters[0].forEach(function () {
				filterStatusList.push(true);
			});

			console.log(filterStatusList);
			pageNumber = 1;
			$scope.preloadMoreResults();

		});

	}

    $scope.preloadMoreResults = function () {

        bookSearchService.search($scope.searchQuery, pageNumber).then(function (response) {
			preloadedResults = $scope.currentBooks.concat(response.data);

            $scope.noMoreSearchResults = !response.data.length;
        });

    }

    $scope.showMoreResults = function () {

		$scope.currentBooks = preloadedResults;
	    pageNumber++;

        $scope.preloadMoreResults();

        };


	// called when a user unticks a filter
	$scope.changeFilterStatus = function(index) {
		filterStatusList[index] = !filterStatusList[index];
		//console.log(filterStatusList);
	}

	$scope.applyFilters = function() {

		var selectedFilters = $scope.searchFilters[0].filter(function (item, index) {
			return filterStatusList[index];
		});


		pageNumber = 0;
		bookSearchService.search($scope.searchQuery, pageNumber, selectedFilters).then(function (response) {
			$scope.currentBooks = response.data;
		});
	}

}]);