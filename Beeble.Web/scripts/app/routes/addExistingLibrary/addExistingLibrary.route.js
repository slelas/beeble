angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('addExistingLibrary', {
			url: '/add-library',
			controller: 'addExistingLibraryController',
			templateUrl: 'scripts/app/routes/addExistingLibrary/addExistingLibrary.template.html',
		});
});