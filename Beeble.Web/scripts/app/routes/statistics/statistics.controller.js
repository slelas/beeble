angular.module('myApp').controller('statisticsController', function ($scope, serviceBase, statisticsService) {

    $scope.numberOfUsersInWeek = 0;

    statisticsService.getLibraryName().then(function (response) {
        $scope.libraryName = response.data;
    });

    $scope.updateYear = function (year) {


        statisticsService.getCategoriesStats().then(function (response) {
            console.log(response.data);
            $scope.labels = response.data[0];
            $scope.data = response.data[1];
        });

        statisticsService.getBorrowedReservedStats(year).then(function (response) {
            console.log(response.data);
            $scope.data2 = response.data;

        });

        statisticsService.getBorrowedInWeek().then(function (response) {
            $scope.labels3 = response.data[0];
            $scope.data3 = [response.data[1]];

            $scope.borrowedInWeekCount = 0;

            for (var i = 0; i < response.data[1].length; i++) {
                $scope.borrowedInWeekCount += parseInt(response.data[1][i]);
            };

            $scope.numberOfUsersInWeek += $scope.borrowedInWeekCount;

        });

        statisticsService.getReservedCountInWeek().then(function (response) {
            $scope.numberOfUsersInWeek += response.data;
        });

        statisticsService.GetLibraryActiveYears().then(function (response) {
            console.log(response.data);
            $scope.years = response.data;
        });

    }

    $scope.updateYear(2017); //debug u current year

    $scope.labels2 = ['Siječanj', 'Veljača', 'Ožujak', 'Travanj', 'Svibanj', 'Lipanj', 'Srpanj', 'Kolovoz', 'Rujan', 'Listopad', 'Studeni', 'Prosinac'];
    $scope.series = ['Posuđene knjige', 'Rezervirane knjige'];

    $scope.colours = ['#B59A56', '#7F6C3D', '#FFD979', '#40361E', '#E5C36D', '#7F6739'];

    $scope.series3 = ['Broj ukupno posuđenih knjiga'];

    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };
    $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
    $scope.options3 = {
        scales: {
        yAxes: [
            {
            id: 'y-axis-1',
            type: 'linear',
            display: true,
            position: 'left'
            }
        ]
        }
    };

    $scope.export = function(){
        statisticsService.getLibraryId().then(function (response) {
            console.log(response.data);
            getExportData(1);
        });
    }

    function getExportData(libraryId) {
        window.location = serviceBase + "api/export/books?libraryId=" + libraryId; 
    }
});