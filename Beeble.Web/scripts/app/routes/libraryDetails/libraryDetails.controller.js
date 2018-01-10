angular.module('myApp').controller('libraryDetailsController',
    function ($scope, $stateParams, getLibrariesService) {

        // called when the state is opened
        ($scope.init = function () {
            getLibrariesService.getLibraryById($stateParams.libraryId).then(function (response) {
                $scope.library = response.data;
                console.log(response.data);
            });
        })();

	});