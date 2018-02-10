angular.module('myApp').config(function ($httpProvider, $locationProvider) {
    $locationProvider.html5Mode(true);
    $httpProvider.interceptors.push('authInterceptorService');
});

angular.module('myApp').run(function ($rootScope) {
    console.log('1234')

    //$scope.$on("routeChangeStart",
    //    function() {
    //        window.scrollTo(0, 0);
    //        console.log('123')
    //    });
});