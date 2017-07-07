/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope','$location'];

    function rootCtrl($scope, $location) {
        //$scope.logOut = function () {
        //    $location.path('/login');
        //}
    }
})(angular.module('webstore'));