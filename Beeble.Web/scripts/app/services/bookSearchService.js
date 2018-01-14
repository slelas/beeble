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

	function getBooksByName(bookName, booksOfLibrariesWithMembership) {

		var deferred = $q.defer();

		$http.get(serviceBase + 'api/search/get-books-byname', { params: { bookName: bookName, booksOfLibrariesWithMembership: booksOfLibrariesWithMembership } })
			.then(function (response) {

			deferred.resolve(response);
		});

		return deferred.promise;
	}

	function getBookNumbers(bookName) {

		var deferred = $q.defer();

		$http.get(serviceBase + 'api/search/get-book-numbers', { params: { bookName: bookName } })
			.then(function (response) {

				deferred.resolve(response);
			});

		return deferred.promise;
	}

   // var selectedFilters = new Array();

    function applyAFilter(filter, searchQuery) {

		//if (!filter)
			var selectedFilters = new Array();

            selectedFilters.push(filter);

		var deferred = $q.defer();
        
        $http.get(serviceBase + 'api/search/byquery', { params: { pageNumber: 0, searchQuery: searchQuery, selectedFilters: selectedFilters} }).then(function (response) {
            deferred.resolve(response);
		});

		return deferred.promise;
    }

    function makeAReservation(libraryId, bookName, authorName) {

        //PRETVORIT U POST
        return $http.get(serviceBase + 'api/search/reserve', { params: { libraryId: libraryId, bookName: bookName, authorName: authorName } }).then(function (results) {
            return results;
        });
    }

	var bookSearchFactory = {};
	bookSearchFactory.search = search;
	bookSearchFactory.getFilters = getFilters;
    bookSearchFactory.getBooksByName = getBooksByName;
	bookSearchFactory.applyAFilter = applyAFilter;
    bookSearchFactory.getBookNumbers = getBookNumbers;
    bookSearchFactory.makeAReservation = makeAReservation;

	return bookSearchFactory;

});