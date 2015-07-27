var registerUserController = function ($scope, $http) {

    $scope.register = function () {

        var newUser = {
            Id: "",
            Login: $scope.login,
            Password: $scope.password,
            FirstName: $scope.firstName,
            LastName: $scope.lastName
        };

        $http.post("api/user/post/", newUser)
          .then(onComplete);
    };

    function onComplete(response) {

        var message = "User sucesfully created";

        if (response.data == 409) {
            message = "Creation failed. User already exists.";
        }

        alert(message);
    }
}