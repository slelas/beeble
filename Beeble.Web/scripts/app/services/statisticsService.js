app.factory('statisticsService',
    function($http, $q, serviceBase) {

        function getCategoriesStats() {

            return $http.get(serviceBase + 'api/libraries/get-categories-stats').then(function(results) {
                return results;
            });
        }

        function getBorrowedReservedStats(year) {

            return $http.get(serviceBase + 'api/libraries/get-borrowed-reserved-monthly', { params: {year: year}}).then(function (results) {
                return results;
            });
        }

        function getBorrowedInWeek() {

            return $http.get(serviceBase + 'api/libraries/get-borrowed-week').then(function (results) {
                return results;
            });
        }
        // debug case camel
        function GetLibraryActiveYears() {

            return $http.get(serviceBase + 'api/libraries/get-active-years').then(function (results) {
                return results;
            });
        }

        function getLibraryId() {

            return $http.get(serviceBase + 'api/libraries/get-library-id').then(function (results) {
                return results;
            });
        }


        var statisticsFactory = {};
        statisticsFactory.getCategoriesStats = getCategoriesStats;
        statisticsFactory.getBorrowedReservedStats = getBorrowedReservedStats;
        statisticsFactory.getBorrowedInWeek = getBorrowedInWeek;
        statisticsFactory.GetLibraryActiveYears = GetLibraryActiveYears;
        statisticsFactory.getLibraryId = getLibraryId;

        return statisticsFactory;
    });