var app = angular.module('myApp', ['ui.router', 'ngDialog', 'ngMessages', 'angular-hidScanner', 'angularjs-dropdown-multiselect', 'ngFileUpload']);

app.run(function (authService) {
    authService.fillAuthData();
});