angular.module('admin', [])
.controller('adminCtrl', function ($scope, $http) {

    $scope.toggleTpl = function (x) {
        $scope.tpl = x;
    };
    $scope.toggleTpl('login');

    $scope.init = function () {
        $scope.islogin = false;
        $scope.u = [];
        sessionStorage.clear();
        $http({
            url: '../Users.asmx/Init',
            method: "POST",
            data: ''
        })
        .then(function (response) {
            $scope.u = JSON.parse(response.data.d);
        },
        function (response) {
            alert(response.data.d)
        });
    }
    $scope.init();

    $scope.login = function (u) {
        sessionStorage.clear();
        $http({
            url: '../Users.asmx/Login',
            method: 'POST',
            data: { userName: u.userName, password: u.password }
        })
        .then(function (response) {
            if (JSON.parse(response.data.d).userName === $scope.u.userName) {
                $scope.u = JSON.parse(response.data.d);
                sessionStorage.setItem("username", $scope.u.userName);
                $scope.islogin = true;
                getProducts($scope.u.userId);
                $scope.toggleTpl('product');
               // window.location.href = "Products.aspx?uid=" + $scope.u.userId + "&type=" + $scope.user.adminType;
            } else {
                alert("Error Login!")
            }
        },
        function (response) {
            alert(response.data.d)
        });
    }

    $scope.signup = function () {
        if ($scope.u.password !== $scope.passwordConfirm) {
            alert("Password do not match.");
            return false;
        }
        $http({
            url: '../Users.asmx/Signup',
            method: "POST",
            data: '{user:' + JSON.stringify($scope.user) + '}'
        })
        .then(function (response) {
            alert(response.data.d)
        },
        function (response) {
            alert(response.data.d)
        });
    }

    var getProducts = function (id) {
        $http({
            url: "../Products.asmx/GetAllProductsByUserId",
            method: 'POST',
            data: { UserId: id },
        })
        .then(function (response) {
            $scope.p = JSON.parse(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var update = function (x) {
        $http({
            url: "../Products.asmx/Update",
            method: 'POST',
            data: { product: x },
        })
        .then(function (response) {
            alert(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var upload = function (x) {
        var content = new FormData(document.getElementById("formUpload"));
        $http({
            url: '../UploadHandler.ashx',
            method: 'POST',
            headers: { 'Content-Type': undefined },
            data: content,
        }).then(function (response) {
            var productId = response.data; // '../upload/' + $scope.u.userId + '/gallery/' + 'todo.png';
            x.gallery = loadProductGallery(productId);

            //if (response.data !== 'OK') {
            //    alert(response.data);
            //}
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var loadProductGallery = function (x) {
        $http({
            url: "../Products.asmx/LoadProductGallery",
            method: 'POST',
            data: { productId: x },
        })
        .then(function (response) {
            return response.data.d;
        },
       function (response) {
           return null; //alert(response.data.d);
       });
    }


    $scope.f = {
        getProducts: function (id) {
            return getProducts(id);
        },
        update: function (x) {
            return update(x)
        },
        upload: function () {
            return upload();
        }

    }

    



})



;