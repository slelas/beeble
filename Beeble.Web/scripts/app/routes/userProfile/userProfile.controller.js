angular.module('myApp').controller('userProfileController', ['$scope', '$state', '$location', 'authService', 'userService', function ($scope, $state, $location, authService) {

	// called when the state is opened
	($scope.init = function () {
		userService.getUser().then(function (response) {
			$scope.user = response.data;
			console.log(response.data);
		});
	})();

}]);