angular.module('myApp').controller('searchController', ['$scope', '$location', '$stateParams', 'authService', 'bookSearchService', function ($scope, $location, $stateParams, authService, bookSearchService) {

    var pageNumber = 0;
	$scope.searchQuery;
    var filterStatusList = new Array();
    var preloadedResults = new Array();
    $scope.selectedFilters = new Array();
    $scope.noMoreSearchResults = true;

	$scope.searchButton = function () {
		$scope.selectedFilters = new Array();
		$scope.search();
    };

	$scope.search = function() {

        pageNumber = 0;
		bookSearchService.search($scope.searchQuery, pageNumber, $scope.selectedFilters).then(function(response) {

            $scope.currentBooks = response.data;
        });

        bookSearchService.getFilters($scope.searchQuery, $scope.selectedFilters).then(function (response) {
            $scope.searchFiltersNationality = response.data[0];
            $scope.searchFiltersAuthor = response.data[1];
            $scope.searchFiltersCategory = response.data[2];
            $scope.searchFiltersYear = response.data[3];
            $scope.searchFiltersLanguage = response.data[4];

            // remove selected filters from available ones
            $scope.searchFiltersNationality[0] = $scope.searchFiltersNationality[0].filter(function (element) {
                return !($scope.selectedFilters.includes(element));
            });
            $scope.searchFiltersAuthor[0] = $scope.searchFiltersAuthor[0].filter(function (element) {
                return !($scope.selectedFilters.includes(element));
            });
            $scope.searchFiltersCategory[0] = $scope.searchFiltersCategory[0].filter(function (element) {
                return !($scope.selectedFilters.includes(element));
            });
            $scope.searchFiltersYear[0] = $scope.searchFiltersYear[0].filter(function (element) {
                return !($scope.selectedFilters.includes(element));
            });
            $scope.searchFiltersLanguage[0] = $scope.searchFiltersLanguage[0].filter(function (element) {
                return !($scope.selectedFilters.includes(element));
            });

            pageNumber = 1;
            $scope.preloadMoreResults();
        });

	    localStorage.setItem('searchQuery', $scope.searchQuery);

	};

	$scope.preloadMoreResults = function() {

		bookSearchService.search($scope.searchQuery, pageNumber, $scope.selectedFilters).then(function(response) {
			preloadedResults = $scope.currentBooks.concat(response.data);
            $scope.noMoreSearchResults = !response.data.length;
		});

	};

    $scope.showMoreResults = function () {

		$scope.currentBooks = preloadedResults;
		pageNumber++;

        $scope.preloadMoreResults();

        };

	$scope.applyAFilter = function (filterName) {
		$scope.selectedFilters.push(filterName);
		$scope.search();
	};

	$scope.removeAFilter = function (filterName) {
		var index = $scope.selectedFilters.indexOf(filterName);
		$scope.selectedFilters.splice(index, 1);
		$scope.search();
	};

    window.scrollTo(0, 0);
    if ($stateParams.searchQuery) {
        $scope.searchQuery = $stateParams.searchQuery;
    }
    else if (!$scope.searchQuery) {
        $scope.searchQuery = localStorage.getItem('searchQuery');
    }
    $scope.search();

}]);