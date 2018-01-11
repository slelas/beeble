angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('libraryHub', {
			url: '/asd',
			controller: 'libraryHubController',
			templateUrl: 'scripts/app/routes/libraryHub/libraryHub.template.html'
		});
});