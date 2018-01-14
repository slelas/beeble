angular.module('myApp').directive('paypal', function () {
    return {
        templateUrl: 'scripts/app/directives/paypalButton/paypalButton.template.html',

        // this directive will inherit its scope from parent
        scope: false
	};
});