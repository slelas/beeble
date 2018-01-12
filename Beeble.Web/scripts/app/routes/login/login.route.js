angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('login', {
            url: '/',
            controller: 'loginController',
			templateUrl: 'scripts/app/routes/login/login.template.html'
        });
});