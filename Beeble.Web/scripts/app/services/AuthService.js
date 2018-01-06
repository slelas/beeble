'use strict';
app.factory('authService', function ($http, $q, serviceBase) {

    var authentication = {
        isAuth: false,
        userName: ""
    };

    function register(registration) {
        logOut();
        console.log(serviceBase);
        return $http.post(serviceBase + 'api/account/register', registration).then(function (response) {
            return response;
        });
    };

    function login(loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
        .then(function (response) {
            localStorage.setItem('authorizationData', JSON.stringify({ token: response.data.access_token, userName: loginData.userName }));

            authentication.isAuth = true;
            authentication.userName = loginData.userName;

            deferred.resolve(response);
        });

        return deferred.promise;
    };

    function logOut() {

        localStorage.removeItem('authorizationData');

        authentication.isAuth = false;
        authentication.userName = "";
    };

    function fillAuthData() {
        var authData = JSON.parse(localStorage.getItem('authorizationData'));
        if (authData) {
            authentication.isAuth = true;
            authentication.userName = authData.userName;
        }
    }


    var authServiceFactory = {};
    authServiceFactory.saveRegistration = register;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
});