angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('members', {
			url: '/members',
			controller: 'membersController',
            templateUrl: 'scripts/app/routes/members/members.template.html',
			parent: 'userLoggedIn'
		});
});