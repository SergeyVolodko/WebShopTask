var loginUserController = function ($scope, $http) {

    $scope.authenticate = function () {
        var data = {
            Login : $scope.login,
            Password : $scope.password
        };
        $http.post("api/loginuser/post/", data)
          .then(onComplete);
    };

    function onComplete(response) {

        if (response.data.NotAuthorized) {
            alert("Wrong login or password");
        }

        var user = response.data;

        $scope.loggedUser = user.Login;
        $scope.firstName = user.FirstName;
        $scope.lastName = user.LastName;
    }
}