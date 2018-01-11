angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('libraryHub', {
			url: '/',
			controller: 'libraryHubController',
			templateUrl: 'scripts/app/routes/libraryHub/libraryHub.template.html'
		});
});