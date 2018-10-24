﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop', ['tedushop.products', 'tedushop.productCategorys', 'tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise('/login');
        
        $stateProvider
        .state('base', {
            url: '',
            templateUrl: '/app/shared/views/baseView.html',
            abstract:true
        })
        .state('login', {
            url: '/login',           
            templateUrl: '/app/components/login/loginView.html',
            controller: 'loginController'
        })
        .state('home', {
            url: '/Admin',
            parent: 'base',
            templateUrl: '/app/components/home/homeView.html',
            controller: 'homeController'
        });;
    }
})();