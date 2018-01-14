angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('addLibrary', {
			url: '/add-library',
			controller: 'addLibraryController',
			templateUrl: 'scripts/app/routes/addLibrary/addLibrary.template.html',
		});
});