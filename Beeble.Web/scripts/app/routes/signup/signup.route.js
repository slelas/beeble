angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('signup', {
            url: '/',
            controller: 'signupController',
            templateUrl: 'scripts/app/routes/signup/signup.template.html',
            onEnter: function ($rootScope) {
                $rootScope.isPageWithValidation = true;
            },
            onExit: function ($rootScope) {
                $rootScope.isPageWithValidation = false;
            }
        });
});