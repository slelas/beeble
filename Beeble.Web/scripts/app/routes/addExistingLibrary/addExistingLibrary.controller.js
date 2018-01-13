angular.module('myApp').controller('addExistingLibraryController',
	function ($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, getLibrariesService) {

		var allLibraries = [];

		getLibrariesService.getAllLibraries().then(function(response) {
			allLibraries = response.data;
			$scope.allSearchLibraries = response.data;
		});

		$scope.$watch('searchQuery',
			function(searchQuery) {
				$scope.allSearchLibraries =
					allLibraries.filter(library => library.name.toLowerCase().indexOf(searchQuery.toLowerCase()) !== -1);
			});

	});