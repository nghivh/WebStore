/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$location'];

    function loginCtrl($scope, $location) {
        $scope.login = function () {
            $location.path('/');
        }
    }
})(angular.module('webstore'));