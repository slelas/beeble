app.factory('statisticsService',
    function($http, $q, serviceBase) {

        function getCategoriesStats() {

            return $http.get(serviceBase + 'api/libraries/get-categories-stats').then(function(results) {
                return results;
            });
        }

        function getBorrowedReservedStats() {

            return $http.get(serviceBase + 'api/libraries/get-borrowed-reserved-monthly').then(function (results) {
                return results;
            });
        }


        var statisticsFactory = {};
        statisticsFactory.getCategoriesStats = getCategoriesStats;
        statisticsFactory.getBorrowedReservedStats = getBorrowedReservedStats;

        return statisticsFactory;
    });