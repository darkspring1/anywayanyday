/**
 * Created by darkspring on 15.02.2015.
 */

angular.module('main')
    .directive('gridView', function () {

        return {
            templateUrl: 'ngapp/main/partial/directives/gridView.html',
            restrict: 'A',

            scope: {
                source: "=",
                rowCount: "@",
                columnCount: "@"
            },

            link: function (scope, element, attrs) {

                function createGrid()
                {
                    var grid = [];
                    for(var i = 0; i < scope.rowCount; i++)
                    {
                        var row = [];
                        for(var j = 0; j < scope.columnCount; j++)
                        {
                            row.push([]);
                        }

                        grid.push(row);
                    }
                    return grid;
                }

                function fillGrid()
                {
                    var rowCount = +scope.rowCount;
                    var columnCount = +scope.columnCount;

                    var count = rowCount * columnCount;

                    if(scope.source > count)
                    {
                        throw "source.lenght > " + count;
                    }

                    _.each(scope.source, function(val, index){
                        var rI = Math.floor(index / columnCount);
                        var cI = index % columnCount;
                        scope.grid[rI][cI].property = val;
                    });
                }

                scope.grid = createGrid();

                scope.$watch("source", function(_new, old){
                    fillGrid();
                });
            }
        }
    });
