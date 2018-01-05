angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('home', {
            url: '/',
            controller: 'homeController',
            templateUrl: 'scripts/app/routes/home/home.template.html'
        });
});