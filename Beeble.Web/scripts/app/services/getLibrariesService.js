app.factory('getLibrariesService', function ($http, serviceBase) {

	function getLibraries() {

		return $http.get(serviceBase + 'api/libraries/get').then(function (results) {
			return results;
		});
	};


	var getLibrariesServiceFactory = {};
	getLibrariesServiceFactory.getLibraries = getLibraries;

	return getLibrariesServiceFactory;

});