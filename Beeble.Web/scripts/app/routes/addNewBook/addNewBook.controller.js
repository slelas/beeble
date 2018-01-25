angular.module('myApp').controller('addNewBookController',
	function($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, serviceBase, Upload) {


		bookSearchService.getAllAuthors().then(function(response) {

			$scope.authors = response.data;
		});

		bookSearchService.getAllCategories().then(function(response) {

			$scope.categories = response.data;
			console.log($scope.categories);
		});

		bookSearchService.getAllNationalities().then(function(response) {

			$scope.nationalities = response.data;
		});

		bookSearchService.getAllLanguages().then(function(response) {

			$scope.languages = response.data;
		});


		$scope.categoriesModel = [];
		$scope.categoriesdata = $scope.categories;
		$scope.categoriessettings = {
			template: '{{option}}',
			checkBoxes: true,
			selectedToTop: true,
			smartButtonMaxItems: 3,
			smartButtonTextConverter: function(itemText, originalItem) { return originalItem; },
			enableSearch: true,
			showCheckAll: false
		};
		$scope.categoriescustomTexts = { buttonDefaultText: 'Odaberi žanrove' };

		var uploadUrl = serviceBase + 'blob';

		$scope.uploadPic = function() {

			var file = $scope.picFile;
			file.upload = Upload.upload({
				url: uploadUrl,
				data: {
					file: $scope.picFile,
					name: $scope.book.name,
					author: $scope.book.author,
					language: $scope.book.language,
					nationality: $scope.book.nationality,
					numOfPages: $scope.book.numOfPages,
					isbn: $scope.book.isbn,
					description: $scope.book.description,
					categories: angular.toJson($scope.categoriesModel)
				}
			});
		}


	});