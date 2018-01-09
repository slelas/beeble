angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('test', {
			url: '/test',
			controller: 'bookDetailsController',
			templateUrl: 'scripts/app/routes/bookDetails/bookDetails.template.html',
		});
});