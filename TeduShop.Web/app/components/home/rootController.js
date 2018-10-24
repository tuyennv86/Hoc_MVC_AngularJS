/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$scope', '$state'];

    function rootController($scope,$state) {

        $scope.logoutSubmit = function () {
            $state.go('login');
        }
    }
})(angular.module('tedushop'));