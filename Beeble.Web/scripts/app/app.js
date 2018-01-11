var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});