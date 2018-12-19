/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.slides', ['tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {        
        $stateProvider
        .state('slides', {
            url: '/slides',
            templateUrl: '/app/components/slides/slideListView.html',
            parent: 'base',
            controller: 'slideListController'
        })
        .state('slides_add', {
            url: '/slides_add',
            templateUrl: '/app/components/slides/slideAddView.html',
            parent: 'base',
            controller: 'slideAddController'
        })
        .state('slides_edit', {
            url: '/slides_edit/:id',
            templateUrl: '/app/components/slides/slideEditView.html',
            parent: 'base',
            controller: 'slideEditController'
        })
    }
})();