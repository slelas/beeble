var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages', 'barcodeListener', 'angular-bar-code-scanner']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});