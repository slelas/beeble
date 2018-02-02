angular.module('myApp').controller('addNewLibraryMemberController',
	function($scope, $stateParams, $state, bookSearchService, ngDialog, $rootScope, authService, serviceBase, Upload) {

        var uploadUrl = serviceBase + 'blob';

        $scope.save = function () {
            console.log('test');
            var file = $scope.picFile;
            file.upload = Upload.upload({
                url: uploadUrl,
                data: {
                    file: $scope.picFile,
                    containerName: 'members',
                    name: $scope.member.name,
                    lastname: $scope.member.lastname,
                    oib: $scope.member.oib,
                    email: $scope.member.email,
                    address: $scope.member.address,
                    city: $scope.member.city,
                    phoneNumber: $scope.member.city
                }
            });
        }
       
	});