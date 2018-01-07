angular.module('myApp').controller('homeController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

    var pageNumber = 0;
    $scope.searchQuery = "A";
    var filterStatusList = new Array();
    var preloadedResults = new Array();
    $scope.noMoreSearchResults = false

	$scope.search = function () {

		bookSearchService.search($scope.searchQuery, pageNumber).then(function(response) {
            $scope.currentBooks = response.data;

            filterStatusList = new Array();
            pageNumber = 0;
            $scope.preloadMoreResults();
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

    $scope.preloadMoreResults = function () {

        bookSearchService.search($scope.searchQuery, ++pageNumber).then(function (response) {
            preloadedResults = $scope.currentBooks.concat(response.data);

            $scope.noMoreSearchResults = !response.data.length;
        });

    }

    $scope.showMoreResults = function () {

        $scope.currentBooks = preloadedResults;

        $scope.preloadMoreResults();

        };


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