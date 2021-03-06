﻿/*!
functions.js
(c) 2019 IG PROG, www.igprog.hr
*/
angular.module('functions', [])
.factory('functions', ['$http', function ($http) {
    return {
        post: (service, method, data) => {
            return $http({
                url: '../' + service + '.asmx/' + method,
                method: 'POST',
                data: data
            })
            .then((response) => {
                return JSON.parse(response.data.d);
            },
            (response) => {
                return response.data.d;
            });
        }
    }
}]);
