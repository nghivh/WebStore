/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict'

    app.controller('productEditCtrl', productEditCtrl);

    productEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'apiService', 'notificationService', '$ngBootbox'];

    function productEditCtrl($scope, $state, $stateParams, apiService, notificationService, $ngBootbox) {
        //$scope.product = {
        //    CreatedDate: new Date(),
        //    HomeFlag: true,
        //    HotFlag: true,
        //    Status: true
        //};
        $scope.onSave = onSave;
        $scope.getSeoTitle = getSeoTitle;
        $scope.loadProduct = loadProduct;

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
            apiService.post('api/products/update', $scope.product,
                saveSucceded,
                saveFailed);
        }

        function saveSucceded(result) {
            notificationService.displaySuccess(result.data.Name + ' đã được chỉnh sửa.');
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
            $scope.moreImages.splice($scope.moreImages.indexOf(image), 1);
        }

        function loadProduct() {
            apiService.get('api/products/getbyid/' + $stateParams.id, null,
            function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            },
            function (error) {
                notificationService.displayError('Load Product không thành công.');
            });
        }

        //Initial on load
        getProductCategories();
        loadProduct();
    }
})(angular.module('webstore'));