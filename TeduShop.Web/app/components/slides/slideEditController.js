/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {

        //$scope.product = {
        //    CreatedDate: new Date()
        //}

        this.isOpen = false;
        $scope.openCalendar = function (e) {
            e.preventDefault();
            e.stopPropagation();
            this.isOpen = true;
        }

        $scope.editorOptions = {
            language: 'vi',
            height: '200px'
        };

        $scope.ChooseImage = function () {

            var finder = new CKFinder();
            finder.selectActionFunction = function (fileURL) {
                $scope.$apply(function () {
                    $scope.product.Image = fileURL;
                })
            }
            finder.popup();
        }
        $scope.MoreImagesList = [];
        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileURL) {
                $scope.$apply(function () {
                    $scope.MoreImagesList.push(fileURL);
                })
            }
            finder.popup();
        }
        $scope.ProductLoadDetail = ProductLoadDetail;

        function ProductLoadDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.MoreImagesList = JSON.parse($scope.product.MoreImages);
            }, function () {
                console.log('Không load được danh mục sản phẩm');
            });
        }

        $scope.RemoveImg = function(item) {
            var index = $scope.MoreImagesList.indexOf(item);
            if (index > -1) {
                $scope.MoreImagesList.splice(index, 1);
            }
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.EditProduct = EditProduct;
        function EditProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.MoreImagesList);
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess('Cập nhật thành công !');
                $state.go('products');
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
        $scope.ProductLoadDetail();
    }
})(angular.module('tedushop.products'));