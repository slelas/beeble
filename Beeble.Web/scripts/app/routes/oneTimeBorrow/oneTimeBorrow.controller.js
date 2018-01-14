var app = angular.module('myApp').controller("oneTimeBorrowController", function ($scope, $state, getLibrariesService, $stateParams, bookSearchService) {

	bookSearchService.getBookForOneTime($stateParams.libraryId, $stateParams.bookName, $stateParams.bookAuthor).then(
		function(response) {
			$scope.book = response.data;
			console.log($scope.book);
		});

});