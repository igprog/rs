﻿angular.module('app', ['ui.router', 'pascalprecht.translate', 'ui.bootstrap', 'functions'])
.config(['$stateProvider', '$urlRouterProvider', '$translateProvider', '$translatePartialLoaderProvider', function ($stateProvider, $urlRouterProvider, $translateProvider, $translatePartialLoaderProvider) {

    $urlRouterProvider.otherwise('/index');

    $stateProvider
        .state('index', {
            url: '/index',
            templateUrl: 'partials/index.html',
            controller: 'appCtrl',
        })
        .state('about', {
            url: '/about',
            templateUrl: 'partials/about.html'  
        });

        $translateProvider.useLoader('$translatePartialLoader', {
            urlTemplate: './json/translations/{lang}/{part}.json'
        });
        $translateProvider.preferredLanguage('en');
        $translatePartialLoaderProvider.addPart('main');   

}])

.controller('appCtrl', ['$scope', '$http', '$translate', '$translatePartialLoader', 'functions', function ($scope, $http, $translate, $translatePartialLoader, functions) {

    $scope.message = "Web page under construction.";
    $scope.today = new Date();
	
	var reloadPage = () => {
        if (typeof (Storage) !== 'undefined') {
            if (localStorage.version) {
                if (localStorage.version !== $scope.config.version) {
                    localStorage.version = $scope.config.version;
                    window.location.reload(true);
                }
            } else {
                localStorage.version = $scope.config.version;
            }
        }
    }

    var getConfig = () => {
        $http.get('../config/config.json').then(function (response) {
              $scope.config = response.data;
              reloadPage();
          });
    };
    getConfig();

    $scope.lang = 'en';
    $scope.setLanguage = function (x) {
        $scope.lang = x;
        $translate.use(x);
        $translatePartialLoader.addPart('main');
    };
    $scope.setLanguage($scope.lang);

    var getCities = () => {
        functions.post('Products', 'GetCities', {}).then((d) => {
            $scope.cities = d;
        });
    }
    getCities();
    
    var getProductGroups = () => {
        functions.post('ProductGroups', 'GetProductGroups', {}).then((d) => {
            $scope.productGroups = d;
        });
    }
    getProductGroups();

    var getProducts = () => {
        functions.post('Products', 'GetProductsByDisplayType', { displayType: 0 }).then((d) => {
            $scope.products = d;
        });
     }
     getProducts();

     var getContents = function () {
         functions.post('Products', 'GetProductsByDisplayType', { displayType: 1 }).then((d) => {
             $scope.contents = d;
         });
     }
     getContents();
  
    /************ Distance *************/
     function distance(lat1, lon1, lat2, lon2, unit) {
         var radlat1 = Math.PI * lat1 / 180
         var radlat2 = Math.PI * lat2 / 180
         var radlon1 = Math.PI * lon1 / 180
         var radlon2 = Math.PI * lon2 / 180
         var theta = lon1 - lon2
         var radtheta = Math.PI * theta / 180
         var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
         dist = Math.acos(dist)
         dist = dist * 180 / Math.PI
         dist = dist * 60 * 1.1515
         if (unit === "K") { dist = dist * 1.609344 }
         if (unit === "N") { dist = dist * 0.8684 }
         return dist.toFixed(2);
     }

    /********* Geolocation ***********/
     var GetGeolocation = function () {
         if (navigator.geolocation) {
             navigator.geolocation.getCurrentPosition(function (position) {
                 $scope.$apply(function () {
                     $scope.position = position;
                     $scope.myLatitude = position.coords.latitude;
                     $scope.myLongitude = position.coords.longitude;
                     angular.forEach($scope.products, function (obj) {
                         obj.distance = distance($scope.myLatitude, $scope.myLongitude, obj.Latitude, obj.Longitude, 'K');
                     });
                 });
             });
         };
     }
     GetGeolocation();
    
    $scope.orderValue = "Distance"
    $scope.sort = function (x) {
        $scope.orderValue = x;
    };

    $scope.filterValue = "";
    $scope.filter = function (x) {
        $scope.filterValue = x;
    };

}])

.controller('productCtrl', ['$scope', '$http', '$translate', 'functions', function ($scope, $http, $translate, functions) {
    var queryString = location.search;
    var params = queryString.split('&');
    if (params[0].substring(1, 3) === 'id') {
        var id = params[0].substring(4);
    }

    var getProduct = (id) => {
        functions.post('Products', 'GetProductByProductId', { productId: id }).then((d) => {
            $scope.p = d;
        });
    }
    getProduct(id);

}])

.directive('sortDirective', function () {
    return {
        restrict: 'E',
        templateUrl: 'partials/filterCtrl.html'
    };
})

.directive('productDirective', function () {
    return {
        restrict: 'E',
        scope: {
            filtercategory: '=',
            products: '=',
            filtervalue: '=',
            ordervalue: '='
        },
        templateUrl: 'partials/products.html'
    };
})

.directive('modalDirective', function () {
    return {
        restrict: 'E',
        scope: {
            img: '='
        },
        templateUrl: 'partials/popup/modal.html'
    };
})

.directive('checkImage', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            attrs.$observe('ngSrc', function (ngSrc) {
                $http.get(ngSrc).success(function () {
                }).error(function () {
                    element.attr('src', './img/default.png'); // set default image
                });
            });
        }
    };
});


;
