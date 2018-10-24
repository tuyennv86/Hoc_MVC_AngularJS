/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productAddController($scope, apiService, notificationService, $state, commonService) {

        $scope.product = {
            CreatedDate: new Date()
        }
        
        this.isOpen = false;

        $scope.openCalendar = function (e) {            
            e.preventDefault();
            e.stopPropagation();
            this.isOpen = true;
        }

        $scope.editorOptions = {
            language: 'vi',
            height:'200px'
        };

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.AddProduct = AddProduct;

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.MoreImagesList);                        
            apiService.post('/api/product/addproduct', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + 'Thêm mới thành công !');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('Lỗi thêm mới không thành công');
                console.log('Không thểm thêm mới lỗi :' + error);
            });
        }
        
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

        $scope.RemoveImg = function (item) {
            var index = $scope.MoreImagesList.indexOf(item);
            if (index > -1) {
                $scope.MoreImagesList.splice(index, 1);
            }
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
})(angular.module('tedushop.products'));