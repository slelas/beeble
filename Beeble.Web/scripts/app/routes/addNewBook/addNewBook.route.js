angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('addNewBook', {
			url: '/new-book',
            controller: 'addNewBookController',
            templateUrl: 'scripts/app/routes/addNewBook/addNewBook.template.html',
			parent: 'userLoggedIn'
		});
});