var app = angular.module('myApp');

app.config(function($stateProvider) {
    $stateProvider.state('home', {
        url: '/test',
        controller: 'homeController',
        templateUrl: 'scripts/app/routes/home/home.template.html'
    })
});