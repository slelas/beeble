angular.module('myApp').controller('bookDetailsController',
	function($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService) {

		$scope.loadBooks = function () {
			bookSearchService.getBooksByName($stateParams.bookName, true).then(function (response) {
			console.log(response.data);
			$scope.memberBooks = response.data[0];
			$scope.nonMemberBooks = response.data[1];
            $scope.book = $scope.nonMemberBooks[0] || $scope.memberBooks[0];
			});
		}

		$scope.getBookNumbers = function() {
			bookSearchService.getBookNumbers($stateParams.bookName).then(function (response) {

				$scope.numberOfAvailableBooks = response.data[0];
				$scope.numberOfReservedBooks = response.data[1];
			});
        }

        $scope.search = function () {
            if ($scope.searchQuery) {
                $state.go('search', { searchQuery: $scope.searchQuery });
            }
        };

        $scope.submitBarcode = function (library) {
            console.log(library.id, $scope.barcodeNumber);
            getLibrariesService.submitBarcode(library.id, $scope.barcodeNumber).then(function (response) {
                console.log(response.data);
            });
        };

        $scope.togglePopup = function (library) {
            $scope.library = library;
            $scope.popupActive = !$scope.popupActive;
            $scope.paymentActive = false;

            if ($scope.popupActive) {
                // Paypal information
                $scope.paymentName =
                    'Library: ' + $scope.library.name +
                    ' (ID: ' + $scope.library.id +
                    ') - MEMBERSHIP';
                $scope.price = $scope.library.membershipPrice;
                $scope.price = 20; //debug
            }
        }

		$scope.loadBooks();
		$scope.getBookNumbers();

		$scope.showReservation = false;

		$scope.toggleReservation = function() {
			$scope.showReservation = false;
		}

        $scope.isLoggedIn = authService.authentication.isAuth;
		$scope.reserveBook = function(libraryName, reservationDuration, libraryId) {

			$rootScope.libraryName = libraryName;
			$rootScope.reservationDuration = reservationDuration;
			console.log(reservationDuration);
			//$scope.showReservation = !$scope.showReservation;
			ngDialog.openConfirm({
				template: 'resolveDialog',
				className: 'ngdialog-theme-default',
				scope: $scope,
				closeByDocument: false,
				closeByEscape: true,
				showClose: false,
				closeByNavigation: true
			});
            console.log(libraryId, $scope.book.name, reservationDuration, $scope.book.author);
            bookSearchService.makeAReservation(libraryId, $scope.book.name, $scope.book.author).then(function (result) {

                $scope.loadBooks();
            });
        };

        $scope.showOneTime = false;

        $scope.toggleOneTime = function () {
            $scope.showOneTime = !$scope.showOneTime;
        }
	});

angular.module('myApp').controller('dialogController', function() {
	
});