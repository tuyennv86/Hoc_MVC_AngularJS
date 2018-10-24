/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams','commonService'];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams,commonService) {
       
        this.isOpen = false;

        $scope.openCalendar = function (e) {
            e.preventDefault();
            e.stopPropagation();
            this.isOpen = true;
        }

        $scope.ProductCategoryLoadDetail = ProductCategoryLoadDetail;

        function ProductCategoryLoadDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;         
            }, function () {
                console.log('Không load được danh mục sản phẩm');
            });
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        $scope.ChooseImage = function () {

            var finder = new CKFinder();
            finder.selectActionFunction = function (fileURL) {
                $scope.$apply(function () {
                    $scope.productCategory.Image = fileURL;
                })                
            }
            finder.popup();
        }

        $scope.EditProductCategory = EditProductCategory;

        function EditProductCategory() {
            apiService.put('/api/productcategory/Update', $scope.productCategory, function (result) {
                notificationService.displaySuccess('Cập nhật thành công !');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Lỗi cập nhật không thành công!');
                console.log('Không thểm cập nhật :' + error);
            });
        }

        $scope.parentCategories = [];
        $scope.loadParentCategory = loadParentCategory;

        function loadParentCategory() {
            apiService.get('/api/productcategory/getallparent', null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                console.log('Không thể lấy được service');
            });
        }

        $scope.loadParentCategory();
        $scope.ProductCategoryLoadDetail();      

    }
})(angular.module('tedushop.productCategorys'));