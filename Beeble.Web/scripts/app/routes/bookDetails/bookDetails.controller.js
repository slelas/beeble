angular.module('myApp').controller('bookDetailsController',
	function ($scope, $stateParams, bookSearchService, ngDialog, $rootScope) {

		$scope.loadBooks = bookSearchService.getBooksByName($stateParams.bookName).then(function(response) {
			console.log(response.data);
			$scope.books = response.data;
			$scope.book = $scope.books[0];
		});

		$scope.reserveBook = function(libraryName, reservationDuration, libraryId) {
			//libraryId po indexu i poslat u backend sa imenom knjige

			$rootScope.libraryName = libraryName;
			$rootScope.reservationDuration = reservationDuration;
			ngDialog.openConfirm({
				template: 'resolveDialog',
				className: 'ngdialog-theme-default',
				scope: $scope,
				closeByDocument: false,
				closeByEscape: true,
				showClose: false,
				closeByNavigation: true
			});

			$scope.loadBooks();

		};

	});

angular.module('myApp').controller('dialogController', function() {
	
});