angular.module('myApp').controller('libraryHubController',
	function ($scope, $stateParams, getLibrariesService) {

		// called when the state is opened
		($scope.init = function () {
			getLibrariesService.getLibraries().then(function (response) {
				$scope.libraries = response.data;
			});
        })();

	});