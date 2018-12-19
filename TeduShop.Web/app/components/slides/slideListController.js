/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getSlide = getSlide;
        $scope.deleteSlide = deleteSlide;
        $scope.keyword = '';
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.deleteMulti = deleteMulti;

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("slides", function (n, o) {
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
            getSlide();
        }

        function deleteMulti() {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không?').then(function () {
                listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                });
                var config = {
                    params: { listId: JSON.stringify(listId) }
                }
                apiService.del('/api/slide/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Đã xóa thành công ' + result.data + ' bản ghi !');
                    getSlide();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });
            });
        }

        function deleteSlide(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa không?').then(function () {

                var config = {
                    params: { id: id }
                }
                apiService.del('/api/slide/delete', config, function (result) {
                    notificationService.displaySuccess('Đã xóa thành công !');
                    getSlide();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                });

            });
        }

        function getSlide(page) {
            page = page || 0;
            keyword = $scope.keyword;
            var config = {
                params: {
                    keyword: keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('/api/slide/getpage', config, function (result) {
                $scope.getSlide = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Không load được danh sách silde');
            });
        }
        $scope.getSlide();
    }
})(angular.module('tedushop.slides'));