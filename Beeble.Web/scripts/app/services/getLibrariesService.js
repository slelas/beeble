app.factory('getLibrariesService', function ($http, serviceBase) {

	function getLibraries() {

		return $http.get(serviceBase + 'api/libraries/get').then(function (results) {
			return results;
		});
	};

    function getLibraryById(libraryId) {

        return $http.get(serviceBase + 'api/libraries/get-byid', { params: { libraryId: libraryId}}).then(function (results) {
            return results;
        });
	};

	function getLibraryByIdForMembership(libraryId) {

		return $http.get(serviceBase + 'api/libraries/get-byid-membership', { params: { libraryId: libraryId } }).then(function (results) {
			return results;
		});
	};

	var getLibrariesServiceFactory = {};
	getLibrariesServiceFactory.getLibraries = getLibraries;
	getLibrariesServiceFactory.getLibraryById = getLibraryById;
	getLibrariesServiceFactory.getLibraryByIdForMembership = getLibraryByIdForMembership;

	return getLibrariesServiceFactory;

});