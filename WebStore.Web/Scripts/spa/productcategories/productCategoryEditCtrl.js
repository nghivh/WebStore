/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.controller('productCategoryEditCtrl', productCategoryEditCtrl);

    productCategoryEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'apiService', 'notificationService', 'commonService'];

    function productCategoryEditCtrl($scope, $state, $stateParams, apiService, notificationService, commonService) {
        $scope.productCategory = {
            UpdatedDate: new Date(),
            Status: true
        };
        $scope.onSave = onSave;
        $scope.getSeoTitle = getSeoTitle;
        $scope.getParentCategories = getParentCategories;
        $scope.loadProductCategory = loadProductCategory;

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
            apiService.post('api/productcategories/update', $scope.productCategory,
                saveSucceded,
                saveFailed);
        }

        function saveSucceded(result) {
            notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
            $state.go("productcategories");
        }

        function saveFailed(error) {
            notificationService.displayError('Chỉnh sửa không thành công.');
        }

        function loadProductCategory() {
            apiService.get('api/productcategories/getbyid/' + $stateParams.id, null,
            function (result) {
                $scope.productCategory = result.data;
            },
            function (error) {
                notificationService.displayError('Load Product Category không thành công.');
            });
        }

        getParentCategories();
        loadProductCategory();
    }

})(angular.module('webstore'));