angular.module('app', ['ui.router', 'pascalprecht.translate', 'ui.bootstrap'])

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


.controller('appCtrl', ['$scope', '$http', '$translate', '$translatePartialLoader', '$modal', function ($scope, $http, $translate, $translatePartialLoader, $modal) {

    $scope.message = "Web page under construction.";

    $scope.lang = 'hr';
    $scope.setLanguage = function (x) {
        $translate.use(x);
        $translatePartialLoader.addPart('main');
    };
    $scope.setLanguage($scope.lang);


    $http({  
        url: "Products.asmx/GetCities",  
        dataType: 'json',  
        method: 'POST',  
        data: {},  
        headers: {  
            "Content-Type": "application/json"  
        }  
    }).success(function (response) {  
        $scope.cities = JSON.parse(response.d);
    })  
    .error(function (error) {   
    }); 


     $http({  
        url: "ProductGroups.asmx/GetProductGroups",  
        dataType: 'json',  
        method: 'POST',  
        data: {},  
        headers: {  
            "Content-Type": "application/json"  
        }  
        }).success(function (response) {  
            $scope.productGroups = JSON.parse(response.d);
        })  
        .error(function (error) {   
        }); 


     var getProducts = function () {
         $http({
             url: "Products.asmx/GetProductsByDisplayType",
             method: 'POST',
             data: { displayType: 0 }
         })
        .then(function (response) {
            $scope.products = JSON.parse(response.data.d);
        },
        function (response) {
            alert(response.data.d);
        });
     }

     var getContents = function () {
         $http({
             url: "Products.asmx/GetProductsByDisplayType",
             method: 'POST',
             data: { displayType: 1 }
         })
        .then(function (response) {
            $scope.contents = JSON.parse(response.data.d);
        },
        function (response) {
            alert(response.data.d);
        });
     }

     getProducts();
     getContents();
  
    //Geolocation
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position){
          $scope.$apply(function(){
            $scope.position = position;
            $scope.myLatitude = position.coords.latitude;
            $scope.myLongitude = position.coords.longitude;
               angular.forEach($scope.products, function(obj){
                        obj.distance = distance($scope.myLatitude, $scope.myLongitude, obj.Latitude, obj.Longitude, 'K');
                    });
          });
        });
      };

  //Distance
  function distance(lat1, lon1, lat2, lon2, unit) {
        var radlat1 = Math.PI * lat1/180
        var radlat2 = Math.PI * lat2/180
        var radlon1 = Math.PI * lon1/180
        var radlon2 = Math.PI * lon2/180
        var theta = lon1-lon2
        var radtheta = Math.PI * theta/180
        var dist = Math.sin(radlat1) * Math.sin(radlat2) + Math.cos(radlat1) * Math.cos(radlat2) * Math.cos(radtheta);
        dist = Math.acos(dist)
        dist = dist * 180/Math.PI
        dist = dist * 60 * 1.1515
        if (unit==="K") { dist = dist * 1.609344 }
        if (unit==="N") { dist = dist * 0.8684 }
        return dist.toFixed(2);
}


    $scope.orderValue = "Distance"
    $scope.sort = function (x) {
        $scope.orderValue = x;
    };

    $scope.filterValue = "";
    $scope.filter = function (x) {
        $scope.filterValue = x;
    };

}])


.controller('productCtrl', ['$scope', '$http', '$translate', function ($scope, $http, $translate) {


    var queryString = location.search;
    var params = queryString.split('&');
    if (params[0].substring(1, 3) === 'id') {
        var id = params[0].substring(4);
    }


    //    //Get cities options
    //    $http.get("json/cities.json").then(function(response) {
    //        $scope.cities = response.data;
    //        console.log('test:' + response.data);
    //    });

    var getProduct = function (id) {
        $http({
            url: "Products.asmx/GetProductByProductId",
            method: 'POST',
            data: { productId: id },
        })
        .then(function (response) {
            debugger;
            $scope.p = JSON.parse(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }
    getProduct(id);


    //var getProductsByDisplayType = function (type) {
    //    $http({
    //        url: "Products.asmx/GetProductsByDisplayType",
    //        method: 'POST',
    //        data: { displayType: type }
    //    })
    //   .then(function (response) {
    //       $scope.contents = JSON.parse(response.d);
    //   },
    //   function (response) {
    //       alert(response.data.d);
    //   });
    //}



}])

  //Product Popup
.controller('productModalPopupCtrl', ['$scope', '$modal', '$http', function ($scope, $modal, $http) {
    $scope.open = function (product) {
        var modalInstance = $modal.open({
            templateUrl: 'partials/popup/productpopup.html',
            controller: 'ProductPopupCtrl',
            resolve: {
                currentProduct: function () {
                    return product;
                }
            }
        });
    }
}])


.controller('ProductPopupCtrl', function ($scope, $modalInstance, currentProduct, $http) {
       
          $scope.g = currentProduct;
       
//        $scope.productId = currentProduct.ProductId;
//        $scope.title = currentProduct.Title;
//        $scope.city = currentProduct.City;
//        $scope.description = currentProduct.Description;
         
          //Get gallery
            $http({  
                url: "Products.asmx/GetGalleryByProductId",  
                dataType: 'json',  
                method: 'POST',  
                data: {productId: currentProduct.productId},  
                headers: {  
                    "Content-Type": "application/json"  
                }  
            }).success(function (response) {  
                $scope.gallery = JSON.parse(response.d);
            })  
            .error(function (error) {  
                alert('Error: ' + error);  
            }); 

            $scope.close = function () {
                $modalInstance.dismiss('cancel');
            };

        })


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
