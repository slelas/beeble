angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('bookDetails', {
			url: '/',
			controller: 'bookDetailsController',
			templateUrl: 'scripts/app/routes/bookDetails/bookDetails.template.html',
			params: {
				bookName: null
			}
		});
});