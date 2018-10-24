/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
        
        function displaySuccess(messasge) {
            toastr.success(messasge);
        }
        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    toastr.error(err);
                })
            } else {
                toastr.error(error);
            }
        }

        function displayWarning(warning) {
            toastr.warning(warning);
        }

        function displayInfo(info) {
            toastr.info(info);
        }
        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }
})(angular.module('tedushop.common'));