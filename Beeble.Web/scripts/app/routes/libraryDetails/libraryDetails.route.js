angular.module('myApp').config(function ($stateProvider) {
	$stateProvider
        .state('libraryDetails', {
            url: '/library-details/:libraryId',
            controller: 'libraryDetailsController',
            templateUrl: 'scripts/app/routes/libraryDetails/libraryDetails.template.html',

			params: {
				libraryId: null
			}
		});
});