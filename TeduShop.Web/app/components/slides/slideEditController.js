/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideEditController', slideEditController);

    slideEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams', 'commonService'];

    function slideEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {


        $scope.editorOptions = {
            language: 'vi',
            height: '200px'
        };

        $scope.ChooseImage = function () {

            var finder = new CKFinder();
            finder.selectActionFunction = function (fileURL) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileURL;
                })
            }
            finder.popup();
        }
       
        $scope.slideLoadDetail = slideLoadDetail;

        function slideLoadDetail() {
            apiService.get('/api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
            }, function () {
                console.log('Không load được danh mục slide');
            });
        }
       
        $scope.EditSlide = EditSlide;
        function EditSlide() {           
            apiService.put('/api/slide/update', $scope.slide, function (result) {
                notificationService.displaySuccess('Cập nhật thành công !');
                $state.go('slides');
            }, function (error) {
                notificationService.displayError('Lỗi cập nhật không thành công!');
                console.log('Không thểm cập nhật :' + error);
            });
        }
       
        $scope.slideLoadDetail();
    }
})(angular.module('tedushop.slides'));