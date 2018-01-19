angular.module('myApp').controller('addNewBookController',
	function($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService) {

        $scope.names = ["Emil", "Tobias", "Linus"];

        bookSearchService.getAllAuthors().then(function (response) {

            $scope.authors = response.data;
        });

        bookSearchService.getAllCategories().then(function (response) {

            $scope.categories = response.data;
            console.log(response.data);
        });

        bookSearchService.getAllNationalities().then(function (response) {

            $scope.nationalities = response.data;
            console.log(response.data);
        });

        bookSearchService.getAllLanguages().then(function (response) {

            $scope.languages = response.data;
            console.log(response.data);
        });


        $scope.categoriesModel = []
        $scope.example1data = $scope.categories;
        $scope.example1settings = {
            template: '{{option}}',
            checkBoxes: true,
            selectedToTop: true,
            smartButtonMaxItems: 3,
            smartButtonTextConverter: function (itemText, originalItem) { return originalItem; },
            enableSearch: true,
            showCheckAll: false
        };
        $scope.example1customTexts = { buttonDefaultText: 'Select Users' };
	});