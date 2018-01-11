app.factory('userService', function ($http, serviceBase) {

	function getUser() {

		/*return $http.get(serviceBase + 'api/libraries/get').then(function (results) {
			return results;
		});*/
	};

	var userServiceFactory = {};
	userServiceFactory.getUser = getUser();

	return userServiceFactory;

});