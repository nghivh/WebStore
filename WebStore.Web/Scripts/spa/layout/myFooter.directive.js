/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.directive('myFooter', myFooter);

    function myFooter() {
        return {
            restrict: 'E',
            replace: true,
            templateUrl: 'scripts/spa/layout/footer.html'
        }
    }
})(angular.module('common.ui'));