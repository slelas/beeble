angular.module('myApp').controller('lendAndReturnController', function ($scope, bookSearchService, getLibrariesService, $window, hidScanner, $timeout) {

	//$scope.barcodeNumberTEST = '1';
	//$scope.memberIdTEST = '9999999999';

    $scope.books = [];
    $scope.member = [];

	//$scope.getScannedBook($scope.barcodeNumberTEST);
 //   $scope.getScannedMember($scope.memberIdTEST);

    $scope.changeScanOption = function (option) {
        scanOption = option;

        $scope.bookScanButton = null;
        $scope.memberScanButton = null;

        if (option === 'book') $scope.bookScanButton = 'activeButton';
        if (option === 'member') $scope.memberScanButton = 'activeButton';

    };

    hidScanner.initialize($scope);

    $scope.changeScanOption('book');

    $scope.processScannedBarcode = function (barcode) {
        console.log('BARCODE IS: ' + barcode);
        console.log(typeof (barcode));

        if (scanOption === 'member') {

            getLibrariesService.getLibraryMember(barcode).then(function (response) {
                console.log(response.data);
                $scope.member = response.data;
                $scope.errorMessage = null;
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

    $scope.memberImageUrl = $scope.member.imageUrl || 'https://www.vccircle.com/wp-content/uploads/2017/03/default-profile.png';

    $scope.confirmScannedItems = function () {
        //debug
        $scope.books = [];
        $scope.member = [];
        $scope.returnMessage = 'Uspješno obavljeno posuđivanje/vraćanje.';
        return 1;

        if (!$scope.member.memberName){
            $scope.books.forEach(function (element) {
                if (!element.isBorrowed) {
                    $scope.errorMessage = "Molimo skenirajte člansku iskaznicu";
                }
            });
        }

        if ($scope.errorMessage)
            return -1;

        if ($scope.member.memberName)
            memberBarcodeNumber = $scope.member.barcodeNumber;
        else
            memberBarcodeNumber = null;

        var bookBarcodes = $scope.books.map(function (item) {
            return item.barcodeNumber;
        });
        console.log(memberBarcodeNumber)
        getLibrariesService.lendAndReturnScanned(bookBarcodes, memberBarcodeNumber).then(function (response) {
            console.log(response.data);
            if (response.data) {
                $scope.books = [];
                $scope.member = [];
            }

            $scope.returnMessage = response.data ?
                'Uspješno obavljeno posuđivanje/vraćanje.' :
                'Došlo je do pogreške. Knjige nisu posuđene ni vraćene.';

            $timeout(function () {
                $scope.returnMessage = null;
            }, 7000);
        });
        
    }

    $scope.removeBook = function (index) {
        $scope.books.splice(index, 1);
    }

    $scope.removeMember = function () {
        $scope.member = [];
    }



    angular.element($window).on('keypress', function (e) {
        console.log(e.key);

        if (e.key == 'c') {
            $scope.books.push(
                {
                    name: 'Superhrana',
                    author: { name: 'Jamie Oliver' },
                    imageUrl: 'http://mozaik-knjiga.hr/wp-content/uploads/2017/10/Superhrana-za-svaki-dan-J.Oliver-234x300.jpg'
                });
        }

        if (e.key == 'v') {
            $scope.books.push(
                {
                    name: 'C# 7.0 za programere',
                    author: { name: 'Joseph Albahari & Ben Albahari' },
                    imageUrl: 'https://covers.oreillystatic.com/images/0636920083634/lrg.jpg'
                });
        }

        if (e.key == 'b') {
            $scope.books.push(
                {
                    name: 'Dizajn danas!',
                    author: { name: 'Charlotte & Peter Fiell' },
                    imageUrl: 'http://verbum.hr/images/artikli/velike/5518.jpg'
                });
        }

        if (e.key == 'n') {
            $scope.member = {
                memberName: 'Josip',
                memberLastName: 'Svalina',
                memberEmail: 'josip@dump.hr',
                memberOib: '95874125896',
                memberAddress: 'Kralja Tomisliava 54, Split',
                numberOfBorrowedBooks: 2,
                numberOfReservedBooks: 1
            };
        }

        $scope.$apply();
    });
});