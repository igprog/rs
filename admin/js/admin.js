﻿angular.module('admin', ['functions'])
.controller('adminCtrl', ['$scope', '$http', 'functions', function ($scope, $http, functions) {

    $scope.f = {
        login: (u) => {
            return login(u);
        },
        logout: () => {
            return logout();
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
        newProduct: (id) => {
            return newProduct(id);
        },
        deleteProduct: (x, u) => {
            return deleteProduct(x, u);
        },
        setMainImg: (x, img) => {
            return setMainImg(x, img);
        }
    }

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

    $scope.toggleTpl = (x) => {
        $scope.tpl = x;
    };
    $scope.toggleTpl('login');

    var init = () => {
        $scope.islogin = false;
        $scope.u = [];
        $scope.p = [];
        sessionStorage.clear();
        functions.post('Users', 'Init', {}).then((d) => {
            $scope.u = d;
        });
    }
    init();

    var login = (u) => {
        sessionStorage.clear();
        functions.post('Users', 'Login', { userName: u.userName, password: u.password }).then((d) => {
            if (d.userName === u.userName) {
                $scope.u = d;
                sessionStorage.setItem("username", $scope.u.userName);
                $scope.islogin = true;
                getProducts($scope.u.userId);
                $scope.toggleTpl('product');
            } else {
                alert("Error Login!");
            }
        });
    }

    var logout = () => {
        init();
        $scope.toggleTpl('login');
    }

    var signup = (u, accept) => {
        if (u.password !== $scope.passwordConfirm) {
            alert("Password do not match.");
            return false;
        }
        if (!accept) {
            alert('confirm terms of service');
            return false;
        }
        functions.post('Users', 'Signup', { user: u }).then((d) => {
            alert(d);
            window.open('./');
        });
    }

    var getProductGroups = (id) => {
        functions.post('ProductGroups', 'GetProductGroups', { }).then((d) => {
            $scope.pg =d;
        });
    }
    getProductGroups();

    var getProducts = (id) => {
        functions.post('Products', 'GetAllProductsByUserId', { userId: id }).then((d) => {
            $scope.p = d;
        });
    }

    var save = (x, u) => {
        functions.post('Products', 'SaveProduct', { product: x, user: u }).then((d) => {
            x.productId = d.productId;
        });
    }

    var upload = (x, idx) => {
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

    var loadProductGallery = (x, idx) => {
        functions.post('Products', 'LoadProductGallery', { productId: x.productId }).then((d) => {
            x.gallery = d;
        });
    }

    var deleteImg = (x, img) => {
        if (confirm('Briši sliku ?')) {
            functions.post('Products', 'DeleteImg', { productId: x.productId, img: img }).then((d) => {
                x.gallery = d;
            });
        }
    }

    var newProduct = (id) => {
        functions.post('Products', 'Init', { userId: id }).then((d) => {
            debugger;
            if (d === 'product limit exceeded') {
                alert(d);
            } else {
                $scope.p.push(d);
            }
        });
    }

    var deleteProduct = (x, u) => {
        if (confirm('Briši oglas ?')) {
            functions.post('Products', 'Delete', { productId: x.productId }).then((d) => {
                getProducts(u.userId);
            });
        }
    }

    var setMainImg = (x, img) => {
        functions.post('Products', 'SetMainImg', { productId: x.productId, img: img }).then((d) => {
            x.image = img;
        });
    }


}])


;