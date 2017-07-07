/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.controller('productCategoryAddCtrl', productCategoryAddCtrl);

    productCategoryAddCtrl.$inject = ['$scope', '$state', '$stateParams', 'apiService', 'notificationService', 'commonService'];

    function productCategoryAddCtrl($scope, $state, $stateParams, apiService, notificationService, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        };
        $scope.onSave = onSave;
        $scope.getSeoTitle = getSeoTitle;

        function getSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function getParentCategories() {
            apiService.get('api/productcategories/getallparents', null,
                function (result) {
                    $scope.parentCategories = result.data;
                },
                function (response) {
                    notificationService.displayError(response.data);
                });
        }

        function onSave() {
            apiService.post('api/productcategories/add', $scope.productCategory,
                saveSucceded,
                saveFailed);
        }

        function saveSucceded(result) {
            notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
            $state.go("productcategories");
        }

        function saveFailed(error) {
            notificationService.displayError('Thêm mới không thành công.');
        }

        getParentCategories();
    }

})(angular.module('webstore'));