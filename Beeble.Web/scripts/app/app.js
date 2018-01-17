var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages', 'angular-hidScanner']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});