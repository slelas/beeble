angular.module('myApp').controller('membersController',
    function ($scope, $stateParams, $state, getLibrariesService, ngDialog, $rootScope, authService) {

        var descending = false;
        var lastPickedOption = null;
        var pageNumber = 0;

        $scope.disableMoreResults = false;

        $scope.setSortOption = function (sortName) {

            if (lastPickedOption !== sortName)
                descending = false;
            else
                descending = !descending;

            lastPickedOption = sortName;
            pageNumber = 0;
            getLibrariesService.getMemberList(sortName, descending, $scope.searchQuery, pageNumber).then(function (response) {
                $scope.members = response.data;
            });

        }

        $scope.search = function () {

            var sortOption = lastPickedOption || 'name';
            pageNumber = 0;
            getLibrariesService.getMemberList(sortOption, descending, $scope.searchQuery, pageNumber).then(function (response) {
                $scope.members = response.data;
            });

            $scope.disableMoreResults = false;

        }

        $scope.setSortOption('name');

        $scope.loadMoreMembers = function () {
            var sortOption = lastPickedOption || 'name';

            getLibrariesService.getMemberList(sortOption, descending, $scope.searchQuery, ++pageNumber).then(function (response) {
                $scope.members = $scope.members.concat(response.data);

                if (response.data.length === 0)
                    $scope.disableMoreResults = true;
            });
        }

        $scope.numberOfLateReturnFees = function (member) {
            return member.borrowedBooks.filter(book => book.lateReturnFee > 0).length;
        }

        $scope.showDetails = function(memberId){
            var member = $scope.members[memberId];
            member.isClicked = !member.isClicked;

        }
	});