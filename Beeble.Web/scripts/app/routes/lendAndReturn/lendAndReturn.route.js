angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('lendAndReturn', {
            url: '/lend-return',
            controller: 'lendAndReturnController',
			templateUrl: 'scripts/app/routes/lendAndReturn/lendAndReturn.template.html'
        });
});