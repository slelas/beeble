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

	function getBookForOneTime(libraryId, bookName, authorName) {
		console.log(libraryId, bookName, authorName);
		return $http.get(serviceBase + 'api/search/get-one-time-borrow', { params: { libraryId: libraryId, bookName: bookName, authorName: authorName } }).then(function (results) {
			return results;
		});
	}

    function getBookById(bookId) {
        console.log('testtt');
		console.log('bookid ' + bookId);
		return $http.get(serviceBase + 'api/search/get-by-id', { params: { bookId: bookId } }).then(function (results) {
			return results;
		});
    }

    function getAllAuthors() {

        return $http.get(serviceBase + 'api/search/get-authors').then(function (results) {
            return results;
        });
    }

    function getAllCategories() {

        return $http.get(serviceBase + 'api/search/get-categories').then(function (results) {
            return results;
        });
    }

    function getAllNationalities() {

        return $http.get(serviceBase + 'api/search/get-nationalities').then(function (results) {
            return results;
        });
    }

    function getAllLanguages() {

        return $http.get(serviceBase + 'api/search/get-languages').then(function (results) {
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
	bookSearchFactory.getBookForOneTime = getBookForOneTime;
    bookSearchFactory.getBookById = getBookById;
    bookSearchFactory.getAllAuthors = getAllAuthors;
    bookSearchFactory.getAllCategories = getAllCategories;
    bookSearchFactory.getAllNationalities = getAllNationalities;
    bookSearchFactory.getAllLanguages = getAllLanguages;

	return bookSearchFactory;

});