/**
 * Created by darkspring on 15.02.2015.
 */
angular.module('main')
    .service("vmService", ["$http", function($http){

        var me = this;

        function getUserId()
        {
            return JSON.parse(localStorage.getItem("uid"));
        }

        me.init = function() {
            var uid = getUserId();
            var params = uid ? { userId: uid } : null;
            return $http.get("api/vendingMachine/init", { params: params })
                .then(function(response) {
                    if(!uid || (uid && uid != response.data.user.id)) { localStorage.setItem("uid", JSON.stringify(response.data.user.id)); }
                    return response;
                });
        };

        me.buy = function(params)
        {
            return $http.post("api/vendingMachine/buy", params );
        }


    }]);