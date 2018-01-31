angular.module('myApp').controller('bookListController',
    function ($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, getLibrariesService) {

        var descending = false;
        var lastPickedOption = null;
        var pageNumber = 0;

        $scope.setSortOption = function (sortName) {

            if (lastPickedOption !== sortName)
                descending = false;
            else
                descending = !descending;

            lastPickedOption = sortName;
            console.log($scope.searchQuery)
            pageNumber = 0;
            getLibrariesService.getBookList(sortName, descending, $scope.searchQuery, pageNumber).then(function (response) {
                console.log(response.data);
                $scope.books = response.data;
            });

        }

        $scope.search = function () {

            var sortOption = lastPickedOption || 'name';
            pageNumber = 0;
            getLibrariesService.getBookList(sortOption, descending, $scope.searchQuery, pageNumber).then(function (response) {
                console.log(response.data);
                $scope.books = response.data;
            });

        }

        $scope.setSortOption('name');

        $scope.loadMoreBooks = function() {
            var sortOption = lastPickedOption || 'name';

            getLibrariesService.getBookList(sortOption, descending, $scope.searchQuery, ++pageNumber).then(function (response) {
                console.log(response.data);
                $scope.books = $scope.books.concat(response.data);
            });
        }
       
	});