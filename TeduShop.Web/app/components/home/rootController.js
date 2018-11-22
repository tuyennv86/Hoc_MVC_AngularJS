/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$scope', '$state', 'loginService', '$injector','authData','authenticationService'];

    function rootController($scope,$state,loginService, $injector, authData, authenticationService) {

        $scope.logOut = function () {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;
        authenticationService.validateRequest();
    }
})(angular.module('tedushop'));