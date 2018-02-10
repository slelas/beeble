angular.module('myApp').config(function ($stateProvider) {
    $stateProvider
        .state('statistics', {
            url: '/statistics',
            controller: 'statisticsController',
            templateUrl: 'scripts/app/routes/statistics/statistics.template.html',
            parent: 'userLoggedIn'
        });
});