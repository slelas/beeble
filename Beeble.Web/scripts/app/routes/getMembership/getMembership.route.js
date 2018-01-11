var app = angular.module('myApp');

app.config(function($stateProvider) {
    $stateProvider.state('getMembership', {
        url: '/enroll/:libraryId',
		controller: 'getMembershipController',
        templateUrl: 'scripts/app/routes/getMembership/getMembership.template.html'
    })
});