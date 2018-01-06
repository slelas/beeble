var app = angular.module('myApp', ['ui.router']);

app.run(function (authService) {

    authService.fillAuthData();
});