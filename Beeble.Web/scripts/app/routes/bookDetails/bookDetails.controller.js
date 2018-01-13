﻿angular.module('myApp').controller('bookDetailsController',
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

		$scope.loadBooks();
		$scope.getBookNumbers();

        $scope.isLoggedIn = authService.isAuth;


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