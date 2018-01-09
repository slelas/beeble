angular.module('myApp').controller('searchController', ['$scope', '$location', 'authService', 'bookSearchService', function ($scope, $location, authService, bookSearchService) {

    var pageNumber = 0;
    $scope.searchQuery = "A";
    var filterStatusList = new Array();
    var preloadedResults = new Array();
    $scope.noMoreSearchResults = false

	$scope.search = function () {

		pageNumber = 0;
        bookSearchService.search($scope.searchQuery, pageNumber).then(function (response) {
            
			$scope.currentBooks = response.data;

		});

		$scope.searchFiltersNationality = [];
		bookSearchService.getFilters($scope.searchQuery).then(function (response) {

            // list of bool values with information which filters have been selected
            filterStatusList = [];

            // nationality filter

			$scope.searchFiltersNationality = response.data[0];

            $scope.searchFiltersNationality[0].forEach(function () {
                filterStatusList.push(true);
			});

            // author filter

            $scope.searchFiltersAuthor = response.data[1];

            $scope.searchFiltersAuthor[0].forEach(function () {
                filterStatusList.push(true);
            });

            // category filter

            $scope.searchFiltersCategory = response.data[2];

            $scope.searchFiltersCategory[0].forEach(function () {
                filterStatusList.push(true);
            });

            //zasto ovo?
            pageNumber = 1;
            $scope.preloadMoreResults();
		});

	}

    $scope.preloadMoreResults = function () {

        bookSearchService.search($scope.searchQuery, pageNumber, $scope.selectedFilters).then(function (response) {
			preloadedResults = $scope.currentBooks.concat(response.data);

            $scope.noMoreSearchResults = !response.data.length;
        });

    }

    $scope.showMoreResults = function () {

		$scope.currentBooks = preloadedResults;
	    pageNumber++;

        $scope.preloadMoreResults();
        console.log($scope.currentBooks);

        };


	// called when a user unticks a filter
	$scope.changeFilterStatus = function(index) {
		filterStatusList[index] = !filterStatusList[index];
		console.log(filterStatusList);
	}

    $scope.applyFilters = function () {

        var allFilters = $scope.searchFiltersNationality[0].concat($scope.searchFiltersAuthor[0]).concat($scope.searchFiltersCategory[0]);

        // selectedFilters takes filters from allFilters which have a checked checkbox
        $scope.selectedFilters = allFilters.filter(function (item, index) {
			return filterStatusList[index];
        });

        console.log(allFilters);


		pageNumber = 0;
        bookSearchService.search($scope.searchQuery, pageNumber, $scope.selectedFilters).then(function (response) {
            $scope.currentBooks = response.data;
            pageNumber++;
            $scope.preloadMoreResults();
		});
	}

}]);