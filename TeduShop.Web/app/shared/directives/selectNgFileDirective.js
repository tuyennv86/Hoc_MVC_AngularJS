(function (app) {
    'use strict';

    app.directive('selectNgFileDirective', selectNgFileDirective);

    function selectNgFileDirective() {
        return {
            require: "ngModel",
            link: function postLink(scope, elem, attrs, ngModel) {
                elem.on("change", function (e) {
                    var files = elem[0].files;
                    ngModel.$setViewValue(files);
                })
            }
        }
    }

})(angular.module('tedushop.common'));