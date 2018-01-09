'use strict';
app.factory('bookSearchService', function ($http, $q, serviceBase) {

	var searchResults;

    function search(searchQuery, pageNumber, selectedFilters/* = ["-1"]*/) {

		var deferred = $q.defer();
 
		$http.get(serviceBase + 'api/search/byquery', { params: { pageNumber: pageNumber, searchQuery: searchQuery, selectedFilters: selectedFilters} }).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
    }

    function getFilters(searchQuery, selectedFilters) {

        var deferred = $q.defer();

		$http.get(serviceBase + 'api/search/get-filters', { params: { searchQuery: searchQuery, selectedFilters: selectedFilters } }).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
	}

	function getBooksByName(bookName) {

		var deferred = $q.defer();

		$http.get(serviceBase + 'api/search/get-books-byname', { params: { bookName: bookName } }).then(function (response) {
			deferred.resolve(response);
		});

		return deferred.promise;
	}

	function applyAFilter(filter) {

		if (!filter)
			allFilters = new Array();

		allFilters.push(filter); // ovo će se slat na api

		/*var deferred = $q.defer();

		$http.get(serviceBase + 'api/search/get-books-byname', { params: { bookName: bookName } }).then(function (response) {
			deferred.resolve(response);
		});

		return deferred.promise;*/
	}


	var bookSearchFactory = {};
	bookSearchFactory.search = search;
	bookSearchFactory.getFilters = getFilters;
	bookSearchFactory.getBooksByName = getBooksByName;

	return bookSearchFactory;

});