angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
		.state('addNewMember', {
			url: '/new-member',
			controller: 'addNewLibraryMemberController',
			templateUrl: 'scripts/app/routes/addNewLibraryMember/addNewLibraryMember.template.html'
		});
});