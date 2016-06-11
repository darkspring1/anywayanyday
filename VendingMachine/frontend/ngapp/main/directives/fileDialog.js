/**
 * Created by darkspring on 15.02.2015.
 */

angular.module('main')
    .directive('fileDialog', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var fileDialogId = attrs.fileDialog;
                var $fileDialog = $("#" + fileDialogId);
                element.on("click", function(){ $fileDialog.trigger("click"); })
            }
        }
    });
