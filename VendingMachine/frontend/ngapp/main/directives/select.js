/**
 * Created by darkspring on 15.02.2015.
 */

angular.module('main')
    .directive('select', function () {
        return {
            templateUrl: 'ngapp/main/partial/directives/select.html',
            restrict: 'A',

            scope: {
                promise: "=",
                default: "=",
                selected: "="
                //afterSelect: "&"
            },

            link: function (scope, element, attrs) {

                scope.promise
                    .then(function(response){
                        scope.items = response.data;
                    });

                scope.onItemClick = function(item) {
                    scope.selected = item;
                    //scope.afterSelect(item);
                }
            }
        }
    });