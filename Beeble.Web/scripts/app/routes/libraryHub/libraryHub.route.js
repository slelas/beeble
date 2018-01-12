angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('libraryHub', {
			url: '/libraryhub',
			controller: 'libraryHubController',
			templateUrl: 'scripts/app/routes/libraryHub/libraryHub.template.html'
		});
});