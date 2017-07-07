/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict'

    app.controller('productAddCtrl', productAddCtrl);

    productAddCtrl.$inject = ['$scope', '$state', 'apiService', 'notificationService', 'commonService', '$ngBootbox'];

    function productAddCtrl($scope, $state, apiService, notificationService, commonService, $ngBootbox) {
        $scope.product = {
            CreatedDate: new Date(),
            HomeFlag: true,
            HotFlag: true,
            Status: true
        };
        $scope.onSave = onSave;
        $scope.getSeoTitle = getSeoTitle;

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.tabclick = function ($event) {
            $event.preventDefault();
        }

        function getSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function getProductCategories() {
            apiService.get('api/productcategories/getallparents', null,
            function (result) {
                $scope.productCategories = result.data;
            },
            function (response) {
                notificationService.displayError(response.data);
            });
        }

        function onSave() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('api/products/add', $scope.product,
                saveSucceded,
                saveFailed);
        }

        function saveSucceded(result) {
            notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
            $state.go("products");
        }

        function saveFailed(error) {
            notificationService.displayError('Thêm mới không thành công.');
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.removeImage = function () {
            $scope.product.Image = " ";
        }

        //Choose More Images
        $scope.moreImages = [];
        $scope.chooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filterUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(filterUrl);
                })
            }
            finder.popup();
        }

        $scope.removeMoreImage = function (image) {
            $scope.moreImages.splice($scope.moreImages.indexOf(image),1);
        }

        //Initial on load
        getProductCategories();
    }
})(angular.module('webstore'));