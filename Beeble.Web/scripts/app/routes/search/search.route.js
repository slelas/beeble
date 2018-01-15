angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('search', {
			url: '/',
			controller: 'searchController',
			templateUrl: 'scripts/app/routes/search/search.template.html',
			parent: 'userLoggedIn',
			params: {
				searchQuery: null
			}
		});
});