var app = angular.module('myApp');

app.config(function($stateProvider) {
	$stateProvider.state('oneTimeBorrow',
		{
			url: '/one-time-borrow/:libraryId/:bookAuthor-:bookName',
			controller: 'oneTimeBorrowController',
            templateUrl: 'scripts/app/routes/oneTimeBorrow/oneTimeBorrow.template.html',
			parent: 'userLoggedIn',
			params: {
				libraryId: null,
				bookAuthor: null,
				bookName: null
			}
		});
});