/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox','$filter'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategory = getProductCategory;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.keyword = '';
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.deleteMulti = deleteMulti;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }              

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                console.log(checked);
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);    
               
        function search() {
            getProductCategory();
        }

        function deleteMulti() {
            listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: { listId: JSON.stringify(listId) }
            }
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                notificationService.displaySuccess('Đã xóa thành công ' + result.data + ' bản ghi !');
                getProductCategory();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không?').then(function () {

                var config = {
                    params: {id:id}
                }
                apiService.del('/api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess('Đã xóa thành công !');
                    getProductCategory();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
                
            });
        }

        function getProductCategory(page) {
            page = page || 0;
            keyword = $scope.keyword;
            var config = {
                params: {
                    keyword: keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
               
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Không load được danh mục sản phẩm');
            });
        }
        $scope.getProductCategory();
    }
})(angular.module('tedushop.productCategorys'));