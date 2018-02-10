angular.module('myApp').controller('userProfileController',
	[
		'$scope', '$state', '$location', 'authService', 'userService','$timeout',
		function($scope, $state, $location, authService, userService, $timeout) {
			userService.getUser().then(function(response) {
				$scope.user = response.data;
            });

            $scope.user = [];
            $scope.userImageUrl = $scope.user.imageUrl || 'https://www.vccircle.com/wp-content/uploads/2017/03/default-profile.png';


            $scope.date = new Date().setHours(0, 0, 0, 0);

			$scope.save = function() {

				$scope.user.userName = $scope.user.email;

				userService.editUser($scope.user)
					.then(function(result) {
				        $scope.message = result.data ? "Podatci su uspješno promijenjeni." : "Podatci nisu promijenjeni. Pokušajte osvježiti stranicu.";
                    });

			    $timeout(function () {
			        $scope.message = null;
			    }, 3000);
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