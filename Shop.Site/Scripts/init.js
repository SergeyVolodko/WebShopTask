(function() {
    var app = angular.module("webShop", []);

    app.controller('registerUserController', ['$scope', '$http', registerUserController]);
    app.controller('loginUserController', ['$scope', '$http', loginUserController]);
}())