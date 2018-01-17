angular.module('myApp').controller('lendAndReturnController', function ($scope, bookSearchService, getLibrariesService, $window) {

	$scope.barcodeNumberTEST = '1';
	$scope.memberIdTEST = '9999999999';

	$scope.books = [];

	$scope.getScannedBook = function (bookId) {

		bookSearchService.getBookById(bookId).then(function(response) {
			console.log(response.data);
			$scope.books.push(response.data);
		});
	}

	$scope.getScannedMember = function (memberId) {

		getLibrariesService.getLibraryMember(memberId).then(function (response) {
			console.log(response.data);
			$scope.member = response.data;
		});
	}

	$scope.getScannedBook($scope.barcodeNumberTEST);
    $scope.getScannedMember($scope.memberIdTEST);

    $scope.setFocus = function () {
        console.log('zes');
        console.log($scope.testInput);
        var name = $window.document.getElementById('name');
            name.focus();
    };
});