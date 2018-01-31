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
                $scope.message = null;
            });
        }
        else if (scanOption === 'book') {

            bookSearchService.getBookByBarcode(barcode).then(function (response) {
                console.log(response.data);

                var bookIds = $scope.books.map(function (item) {
                    return item.id;
                });

                // check if book has already been scanned
                //if (bookIds.indexOf(response.data.id) === -1)
                    $scope.books.push(response.data);
            });
        }
    };

    $scope.confirmScannedItems = function () {

        if ($scope.member == null){
            $scope.books.forEach(function (element) {
                if (!element.isBorrowed) {
                    $scope.message = "Molimo skenirajte člansku iskaznicu";
                }
            });
        }

        if ($scope.message)
            return -1;

        if ($scope.member)
            memberBarcodeNumber = $scope.member.barcodeNumber;
        else
            memberBarcodeNumber = null;

        var bookBarcodes = $scope.books.map(function (item) {
            return item.barcodeNumber;
        });

        getLibrariesService.lendAndReturnScanned(bookBarcodes, memberBarcodeNumber).then(function (response) {
            console.log(response.data);
        });
        
    }
});