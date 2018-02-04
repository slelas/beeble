angular.module('myApp').controller('statisticsController', function ($scope, serviceBase) {
    $scope.labels = ["Programiranje", "Dizajn", "Kuharica", "Drama", "Biografija", "Roman"];
    $scope.data = [4, 8, 6, 3, 1, 1];

    $scope.labels2 = ['Siječanj', 'Veljača', 'Ožujak', 'Travanj', 'Svibanj', 'Lipanj', 'Srpanj', 'Kolovoz', 'Rujan', 'Listopad', 'Studeni', 'Prosinac'];
    $scope.series = ['Posuđene knjige', 'Rezervirane knjige'];

    $scope.colours = ['#B59A56', '#7F6C3D', '#FFD979', '#40361E', '#E5C36D', '#7F6739'];

    $scope.data2 = [
    [32, 45, 78, 28, 25, 50, 60, 63, 90, 48, 66, 69],
    [15, 28, 23, 68, 58, 38, 77, 93, 19, 23, 78, 50]
    ];

    $scope.labels3 = ['Ponedjeljak', 'Utorak', 'Srijeda', 'Četvrtak', 'Petak', 'Subota', 'Nedjelja'];
    $scope.series3 = ['Broj ukupno posuđenih knjiga'];
    $scope.data3 = [
        [10, 15, 23, 28, 34, 48, 72]
    ];

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
        window.location.href = serviceBase + "content/podatci.xlsx";
    }
});