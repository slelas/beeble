angular.module('myApp').controller('userProfileController',
	[
		'$scope', '$state', '$location', 'authService', 'userService',
		function($scope, $state, $location, authService, userService) {
			userService.getUser().then(function(response) {
				$scope.user = response.data;
				console.log(response.data);
			});

			$scope.save = function() {

				$scope.user.userName = $scope.user.email;

				userService.editUser($scope.user)
					.then(function(result) {
						console.log(result);
					});
			}

            $scope.tabName = 'profile';
            $scope.profileTab = "activeTab";

            $scope.switchTab = function (tabToSwitch) {

                $scope.tabName = tabToSwitch;

                $scope.profileTab = null;
                $scope.borrowedTab = null;
                $scope.reservedTab = null;

                if ($scope.tabName === 'profile')
                    $scope.profileTab = "activeTab";
                else if ($scope.tabName === 'borrowedBooks')
                    $scope.borrowedTab = "activeTab";
                else if ($scope.tabName === 'reservedBooks')
                    $scope.reservedTab = "activeTab";

            };

		}
	]);