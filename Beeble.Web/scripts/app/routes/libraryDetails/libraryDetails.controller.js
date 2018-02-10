angular.module('myApp').controller('libraryDetailsController',
    function ($scope, $stateParams, getLibrariesService, barcodeGeneratorBase) {

        // called when the state is opened
        ($scope.init = function () {
            getLibrariesService.getLibraryById($stateParams.libraryId).then(function (response) {
                $scope.library = response.data;

				$scope.barcodeGeneratorFull = barcodeGeneratorBase + $scope.library.memberId + '.jpg';
            });
		})();

    });