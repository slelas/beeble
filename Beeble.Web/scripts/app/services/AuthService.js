'use strict';
app.factory('authService', ['$http', '$q', function ($http, $q) {

    var serviceBase = 'http://localhost:58254/';
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: ""
    };

    var _saveRegistration = function (registration) {

        _logOut();

        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            console.log(response);
            return response;

        });

    };

    var _login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (response) {
            console.log(response);
            localStorage.setItem('authorizationData', JSON.stringify({ token: response.data.access_token, userName: loginData.userName }));

            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;

            deferred.resolve(response);

        }).catch(function (err, status) {
            // auto log out kod errora
            _logOut();
            console.log('test');
            console.log(err)
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorage.removeItem('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

        console.log('LOGGED OUT');

    };

    var _fillAuthData = function () {

        var authData = JSON.parse(localStorage.getItem('authorizationData'));
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }

    }

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);