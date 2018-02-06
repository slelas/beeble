app.factory('statisticsService',
    function($http, $q, serviceBase) {

        function getCategoriesStats() {

            return $http.get(serviceBase + 'api/libraries/get-categories-stats').then(function(results) {
                return results;
            });
        }


        var statisticsFactory = {};
        statisticsFactory.getCategoriesStats = getCategoriesStats;

        return statisticsFactory;
    });