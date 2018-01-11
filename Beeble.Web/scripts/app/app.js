var app = angular.module('myApp', ['ui.router', 'ngDialog']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});