angular.module('myApp').controller('bookDetailsController',
	function ($scope, $stateParams, bookSearchService) {
		 $scope.books = bookSearchService.getBooksByName($stateParams.bookName).then(function(response) {
			console.log(response.data);
		 });

		$scope.book = {
			name: "Steve Jobs",
			author: "Walter Isaacson",
			numOfPages: 623,
			yearOfIssue: 2015,
			ISBN: "978-953-300-223-1",
			description: 'Na osnovi više od četrdeset intervjua s Jobsom vodenih u dvije godine — kao i razgovora s više od stotinu članova obitelji, prijatelja, suparnika, konkurenata i kolega -Walter Isaacson napisao je uzbudljivu priču o vrtoglavom životu i žestokom karakteru kreativnog poduzetnika čiji su strastveni perfekcionizam i bespoštedna energija revolucionalizirali šest industrija: kompjutore, animirane filmove, glazbu, mobitele, tabletne kompjutore i digitalno izdavaštvo.',
			publisher: 'Europapress holding, Zagreb',
			language: 'hrvatski'
		}
	});