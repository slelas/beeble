var app = angular.module('myApp', ['ui.router']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});