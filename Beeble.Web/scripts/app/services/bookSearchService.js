'use strict';
app.factory('bookSearchService', function ($http, $q, serviceBase) {

	var searchResults;

    function search(searchQuery, pageNumber) {

        var deferred = $q.defer();
 
		$http.get(serviceBase + 'api/search/byquery', { params: { pageNumber: pageNumber, searchQuery: searchQuery} }).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
    }

    function getFilters(searchQuery) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/search/get-filters', { params: { search: searchQuery } }).then(function (response) {
            deferred.resolve(response);
        });

        return deferred.promise;
    }


	var bookSearchFactory = {};
	bookSearchFactory.search = search;

	return bookSearchFactory;

});