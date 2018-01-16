var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages', 'barcodeListener']);

app.run(function (authService, $http) {

    authService.fillAuthData();
});