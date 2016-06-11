/**
 * Created by darkspring on 15.02.2015.
 */
angular.module('business')
    .service("wallet", ["$http", function($http){


        function wallet(){
            var me = this;
            me.r1 = 0;
            me.r2 = 0;
            me.r5 = 0;
            me.r10 = 0;
        }

        var me = this;

        function getProp(coin)
        {
            switch (coin) {
                case 1:
                    return "r1";
                case 2:
                    return "r2";
                case 5:
                    return "r5";
                case 10:
                    return "r10";
                default : console.log("bad coin: " + coin);
            }
        }

        me.create = function()
        {
            return new wallet();
        }

        me.moveOneCoin = function(destinationW, sourceW, coin)
        {
            var prop = getProp(coin);
            if(sourceW[prop] <= 0)
            {
                console.log("No coins in source. Coin: " + coin);
            }
            else{
                destinationW[prop]++;
                sourceW[prop]--;
            }
        }

        me.moveAllCoins = function(destinationW, sourceW)
        {
            destinationW.r1 += sourceW.r1;
            destinationW.r2 += sourceW.r2;
            destinationW.r5 += sourceW.r5;
            destinationW.r10 += sourceW.r10;
            me.clear(sourceW);
        }

        me.clear = function(w)
        {
            w.r1 = 0;
            w.r2 = 0;
            w.r5 = 0;
            w.r10 = 0;
        }

        me.total = function(w)
        {
            return w.r1 + w.r2 * 2 + w.r5 * 5 + w.r10 * 10;
        }


    }]);