'use strict';
app.factory('bookSearchService', function ($http, $q, serviceBase) {

	var searchResults;

	function search(searchQuery, pageNumber) {
		console.log('search');
		return $http.get(serviceBase + 'api/search/byquery', { params: { pageNumber: pageNumber, search: searchQuery} }).then(function (response) {
			console.log(response);
			return response;
		});
	}

	var bookSearchFactory = {};
	bookSearchFactory.search = search;

	return bookSearchFactory;

});