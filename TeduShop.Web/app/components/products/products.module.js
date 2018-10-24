/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tedushop.products', ['tedushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {        
        $stateProvider
            .state('products', {
                url: '/products',
                templateUrl: '/app/components/products/productListView.html',
                parent: 'base',
                controller: 'productListController'
            })
        .state('products_add', {
            url: '/products_add',
            templateUrl: '/app/components/products/productAddView.html',
            parent: 'base',
            controller: 'productAddController'
        })
        .state('products_edit', {
            url: '/products_edit/:id',
            templateUrl: '/app/components/products/productEditView.html',
            parent: 'base',
            controller: 'productEditController'
        })
    }
})();