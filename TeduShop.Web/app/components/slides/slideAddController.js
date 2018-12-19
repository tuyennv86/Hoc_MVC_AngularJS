/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function slideAddController($scope, apiService, notificationService, $state, commonService) {
               
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
       
        $scope.AddSlide = AddSlide;

        function AddSlide() {                                   
            apiService.post('/api/slide/addslide', $scope.slide, function (result) {
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
                    $scope.slide.Image = fileURL;
                })
            }
            finder.popup();
        }       

    }
})(angular.module('tedushop.products'));