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

		$scope.searchFiltersNationality = [];
		bookSearchService.getFilters($scope.searchQuery).then(function (response) {

			filterStatusList = [];

			$scope.searchFiltersNationality = response.data[0];

            for (var i = 0; i < $scope.searchFiltersNationality.length; i++) {
                console.log($scope.searchFiltersNationality[i]);
            };


            $scope.searchFiltersNationality[0].forEach(function () {
                filterStatusList.push(true);
			});

            console.log(filterStatusList);
			pageNumber = 1;
			$scope.preloadMoreResults();

            //author filter

            $scope.searchFiltersAuthor = response.data[1];

            for (var i = 0; i < $scope.searchFiltersAuthor.length; i++) {
                console.log($scope.searchFiltersAuthor[i]);
            };


            $scope.searchFiltersAuthor[0].forEach(function () {
                filterStatusList.push(true);
            });

            console.log(filterStatusList);
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

        };


	// called when a user unticks a filter
	$scope.changeFilterStatus = function(index) {
		filterStatusList[index] = !filterStatusList[index];
		console.log(filterStatusList);
	}

    $scope.applyFilters = function () {

        var allFilters = $scope.searchFiltersNationality[0].concat($scope.searchFiltersAuthor[0]);

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