var app = angular.module('myApp').controller("oneTimeBorrowController", function ($scope, $state, getLibrariesService, $stateParams, bookSearchService) {

	bookSearchService.getBookForOneTime($stateParams.libraryId, $stateParams.bookName, $stateParams.bookAuthor).then(
		function(response) {
            $scope.book = response.data;

            // Paypal information
            $scope.paymentName =
                'Library: ' + $scope.book.localLibrary.name +
                ' (ID: ' + $scope.book.localLibrary.id +
                ') - ONE-TIME BORROWING';
            $scope.price = $scope.book.localLibrary.guestBorrowPrice;

		});

});