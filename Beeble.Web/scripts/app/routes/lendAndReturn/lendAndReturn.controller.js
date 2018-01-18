angular.module('myApp').controller('lendAndReturnController', function ($scope, bookSearchService, getLibrariesService, $window, hidScanner) {

	//$scope.barcodeNumberTEST = '1';
	//$scope.memberIdTEST = '9999999999';

    $scope.books = [];

    var scanOption = "book";

	//$scope.getScannedBook($scope.barcodeNumberTEST);
 //   $scope.getScannedMember($scope.memberIdTEST);

    $scope.changeScanOption = function (option) {
        scanOption = option;
    };

    hidScanner.initialize($scope);

    $scope.processScannedBarcode = function (barcode) {
        console.log('BARCODE IS: ' + barcode);
        console.log(typeof (barcode));

        if (scanOption === 'member') {

            getLibrariesService.getLibraryMember(barcode).then(function (response) {
                console.log(response.data);
                $scope.member = response.data;
            });
        }
        else if (scanOption === 'book') {

            bookSearchService.getBookById(barcode).then(function (response) {
                console.log(response.data);

                // check if book has already been scanned
                if ($scope.books.indexOf(response.data) === -1)
                    $scope.books.push(response.data);
            });
        }
    };
});