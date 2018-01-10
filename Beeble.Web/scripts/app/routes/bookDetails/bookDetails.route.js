angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('test', {
			url: '/',
			controller: 'bookDetailsController',
			templateUrl: 'scripts/app/routes/bookDetails/bookDetails.template.html',
		});
});