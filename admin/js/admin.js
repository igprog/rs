angular.module('admin', ['functions'])
.controller('adminCtrl', ['$scope', '$http', 'functions', function ($scope, $http, functions) {

    $scope.toggleTpl = function (x) {
        $scope.tpl = x;
    };
    $scope.toggleTpl('login');

    var init = () => {
        $scope.islogin = false;
        $scope.u = [];
        sessionStorage.clear();
        functions.post('Users', 'Init', {}).then((d) => {
            $scope.u = d;
        });
    }
    init();

    var login = function (u) {
        sessionStorage.clear();
        functions.post('Users', 'Login', { userName: u.userName, password: u.password }).then((d) => {
            if (d.userName === u.userName) {
                $scope.u = d;
                sessionStorage.setItem("username", $scope.u.userName);
                $scope.islogin = true;
                getProducts($scope.u.userId);
                $scope.toggleTpl('product');
                // window.location.href = "Products.aspx?uid=" + $scope.u.userId + "&type=" + $scope.user.adminType;
            } else {
                alert("Error Login!");
            }
        });
    }

    //var login = function (u) {
    //    sessionStorage.clear();
    //    $http({
    //        url: '../Users.asmx/Login',
    //        method: 'POST',
    //        data: { userName: u.userName, password: u.password }
    //    })
    //    .then(function (response) {
    //        if (JSON.parse(response.data.d).userName === u.userName) {
    //            $scope.u = JSON.parse(response.data.d);
    //            sessionStorage.setItem("username", $scope.u.userName);
    //            $scope.islogin = true;
    //            getProducts($scope.u.userId);
    //            $scope.toggleTpl('product');
    //           // window.location.href = "Products.aspx?uid=" + $scope.u.userId + "&type=" + $scope.user.adminType;
    //        } else {
    //            alert("Error Login!");
    //        }
    //    },
    //    function (response) {
    //        alert(response.data.d);
    //    });
    //}

    var signup = function (u, accept) {
        if (u.password !== $scope.passwordConfirm) {
            alert("Password do not match.");
            return false;
        }
        if (!accept) {
            alert('confirm terms of service');
            return false;
        }
        $http({
            url: '../Users.asmx/Signup',
            method: "POST",
            data: { user: u }
        })
        .then(function (response) {
            alert(response.data.d)
        },
        function (response) {
            alert(response.data.d)
        });
    }

    var getProductGroups = function (id) {
        $http({
            url: "../ProductGroups.asmx/GetProductGroups",
            method: 'POST',
            data: '',
        })
        .then(function (response) {
            $scope.pg = JSON.parse(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }
    getProductGroups();

    var getProducts = function (id) {
        $http({
            url: "../Products.asmx/GetAllProductsByUserId",
            method: 'POST',
            data: { userId: id },
        })
        .then(function (response) {
            $scope.p = JSON.parse(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var save = function (x, u) {
        $http({
            url: "../Products.asmx/SaveProduct",
            method: 'POST',
            data: { product: x, user: u },
        })
        .then(function (response) {
            x.productId = JSON.parse(response.data.d).productId;
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var upload = function (x, idx) {
        var content = new FormData(document.getElementById('formUpload_' + x.productId));
        $http({
            url: '../UploadHandler.ashx',
            method: 'POST',
            headers: { 'Content-Type': undefined },
            data: content,
        }).then(function (response) {
           loadProductGallery(x, idx);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var loadProductGallery = function (x, idx) {
        $http({
            url: "../Products.asmx/LoadProductGallery",
            method: 'POST',
            data: { productId: x.productId },
        })
        .then(function (response) {
            x.gallery = JSON.parse(response.data.d);
        },
       function (response) {
            alert(response.data.d);
       });
    }

    var deleteImg = function (x, img) {
        $http({
            url: "../Products.asmx/DeleteImg",
            method: 'POST',
            data: { productId: x.productId, img: img },
        })
        .then(function (response) {
            x.gallery = JSON.parse(response.data.d);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var newProduct = function () {
        $http({
            url: "../Products.asmx/Init",
            method: 'POST',
            data: '',
        })
        .then(function (response) {
            $scope.p.push(JSON.parse(response.data.d));
        },
       function (response) {
           alert(response.data.d);
       });
    }

    var deleteProduct = function (x, u) {
        $http({
            url: "../Products.asmx/Delete",
            method: 'POST',
            data: { productId: x.productId },
        })
        .then(function (response) {
            getProducts(u.userId);
        },
       function (response) {
           alert(response.data.d);
       });
    }

    $scope.f = {
        login: (u) => {
            return login(u);
        },
        logout: () => {
            return init();
        },
        signup: (u, accept) => {
            return signup(u, accept);
        },
        getProducts: (id) => {
            return getProducts(id);
        },
        save: (x, u) => {
            return save(x, u)
        },
        upload: (x, idx) => {
            return upload(x, idx);
        },
        deleteImg: (x, img) => {
            return deleteImg(x, img);
        },
        newProduct: () => {
            return newProduct();
        },
        deleteProduct: (x, u) => {
            return deleteProduct(x, u);
        }
    }

}])


;