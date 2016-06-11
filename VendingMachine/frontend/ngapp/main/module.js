/**
 * Created by darkspring on 15.02.2015.
 */

angular.module('main', ['ui.router', 'ui.select', 'business'])
    .config(['$provide', '$httpProvider', '$locationProvider', '$urlRouterProvider', '$stateProvider', function ($provide, $httpProvider, $locationProvider, $urlRouterProvider, $stateProvider) {

        $locationProvider.html5Mode(true);
        $urlRouterProvider.otherwise('/main');

        $stateProvider
            .state('root', {
                url: '',
                template: '<ui-view />'
            })

            .state('root.main', {
                url: '/main',
                templateUrl: 'ngapp/main/partial/main.html',
                controller: ['$scope', '$state', 'vmService', "wallet", function ($scope, $state, vmService, wallet) {

                    var cashBox = wallet.create();

                    vmService.init()
                        .then(function(response){
                            $scope.m = response.data;
                            $scope.cash = 0;
                        });

                    $scope.moveOneCoinToCashbox = function(coin)
                    {
                        wallet.moveOneCoin(cashBox, $scope.m.user.wallet, coin);
                        $scope.cash = wallet.total(cashBox);
                    }

                    $scope.cancel = function() {
                        wallet.moveAllCoins($scope.m.user.wallet, cashBox);
                        $scope.cash = 0;
                        $scope.m.selectedGood = null;
                    }

                    $scope.buy = function() {
                        vmService.buy({
                            userId: $scope.m.user.id,
                            vendingMachineId: $scope.m.vendingMachine.id,
                            goodId: $scope.m.selectedGood.id,
                            cashBox: cashBox
                        }).then(function (response) {

                            var responseCode = {
                                ok: "Ok",
                                smallCash: "SmallCash",
                                noTrifle: "NoTrifle",
                                noGood: "NoGood"
                            }


                            switch(response.data.code)
                            {
                                case responseCode.ok:
                                    $scope.cash = 0;
                                    wallet.clear(cashBox);
                                    $scope.m = response.data;
                                    alert('Возьмите напиток!');
                                    break;
                                case responseCode.smallCash:
                                    $scope.cancel();
                                    alert('Недостаточно денег');
                                    break;
                                case responseCode.noTrifle:
                                    $scope.cancel();
                                    alert('Нет сдачи');
                                    break;
                                case responseCode.noGood:
                                    $scope.cancel();
                                    alert('Выбранный напиток закончился');
                                    break;
                            }


                        });
                    };

                }]
            })


    }])
    .run(['$rootScope', '$state', '$stateParams',
        function ($rootScope, $state, $stateParams) {

        }]);
