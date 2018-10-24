/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date()
        }

        this.isOpen = false;

        $scope.openCalendar = function (e) {
            e.preventDefault();
            e.stopPropagation();
            this.isOpen = true;
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

        $scope.AddProductCategory = AddProductCategory;

        function AddProductCategory() {
            apiService.post('/api/productcategory/Add', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + 'Thêm mới thành công !');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Lỗi thêm mới không thành công');
                console.log('Không thểm thêm mới lỗi :' + error);
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



    }
})(angular.module('tedushop.productCategorys'));