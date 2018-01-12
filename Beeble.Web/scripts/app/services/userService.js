app.factory('userService', function ($http, serviceBase) {

	function getUser() {

		return $http.get(serviceBase + 'api/account/get').then(function (results) {
			return results;
		});
	};

	function editUser(user) {
		return $http.post(serviceBase + 'api/account/edit', user);
	}

	var userServiceFactory = {};
	userServiceFactory.getUser = getUser;
	userServiceFactory.editUser = editUser;

	return userServiceFactory;

});