angular.module('myApp').controller('membersController',
    function ($scope, $stateParams, $state, getLibrariesService, ngDialog, $rootScope, authService) {

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
            getLibrariesService.getMemberList(sortName, descending, $scope.searchQuery, pageNumber).then(function (response) {
                console.log(response.data);
                $scope.members = response.data;
            });

        }

        $scope.search = function () {

            var sortOption = lastPickedOption || 'name';
            pageNumber = 0;
            getLibrariesService.getMemberList(sortOption, descending, $scope.searchQuery, pageNumber).then(function (response) {
                console.log(response.data);
                $scope.members = response.data;
            });

        }

        $scope.setSortOption('name');

        $scope.loadMoreMembers = function () {
            var sortOption = lastPickedOption || 'name';

            getLibrariesService.getMemberList(sortOption, descending, $scope.searchQuery, ++pageNumber).then(function (response) {
                console.log(response.data);
                $scope.members = $scope.members.concat(response.data);
            });
        }

        $scope.numberOfLateReturnFees = function (member) {
            return member.borrowedBooks.filter(book => book.lateReturnFee > 0).length;
        }

	});