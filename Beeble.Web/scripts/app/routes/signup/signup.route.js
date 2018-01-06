angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('signup', {
            url: '/signup',
            controller: 'signupController',
            templateUrl: 'scripts/app/routes/signup/signup.template.html'
        });
});