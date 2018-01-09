angular.module('myApp').controller('bookDetailsController',
	function ($scope, $stateParams, bookSearchService) {

		$scope.books = bookSearchService.getBooksByName($stateParams.bookName).then(function(response) {
			console.log(response.data);
		});

	});