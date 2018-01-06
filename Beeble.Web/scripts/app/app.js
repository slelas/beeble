var app = angular.module('myApp', ['ui.router']);

app.run(function (authService, $http) {

    authService.fillAuthData();

    $http.get('http://localhost:58718/' + 'api/search/get-filters', { params: { searchQuery: 'S' } }).then(function (response) {
        console.log(response.data);
    });
});