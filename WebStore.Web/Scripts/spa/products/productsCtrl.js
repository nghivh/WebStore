/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict'
    app.controller('productsCtrl', productsCtrl);

    productsCtrl.$inject = ['$scope', '$state', 'apiService', 'notificationService', '$ngBootbox'];

    function productsCtrl($scope, $state, apiService, notificationService, $ngBootbox) {
        $scope.products = [];
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.loadingState = false;

        $scope.search = search;
        $scope.onRefresh = onRefresh;
        $scope.onEdit = onEdit;
        $scope.onDelete = onDelete;
        $scope.onDeleteMulti = onDeleteMulti;
        $scope.selectAll = selectAll;

        function search(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: $scope.pageSize,
                    filter: $scope.filterString
                }
            }

            apiService.get('api/products/getall', config,
                getSuccess,
                getFailure);
        }

        function getSuccess(result) {
            $scope.products = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalsPage;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingState = false;
        }

        function getFailure(response) {
            notificationService.displayError(response.data);
            $scope.loadingState = false;
        }

        function onRefresh() {
            search();
        }

        function onEdit(id) {
            $state.go("product_edit", { id: id });
        }

        function onDelete(id) {
            $ngBootbox.confirm("Bạn có chắc chắn muốn xóa?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }

                apiService.del('api/products/delete', config,
                    function () {
                        notificationService.displaySuccess("Xóa thành công");
                        search();
                    },
                    function () {
                        notificationService.displayError("Xóa không thành công");
                    });
            });
        }

        function onDeleteMulti() {
            $ngBootbox.confirm("Bạn có chắc chắn muốn xóa tất cả?").then(function () {
                var checkedId = [];
                angular.forEach($scope.products, function (item) {
                    if (item.checked === true) {
                        checkedId.push(item.ID);
                    }
                });

                var config = {
                    params:{
                        checkedId: JSON.stringify(checkedId)
                    }
                }

                apiService.del('api/products/deleteMulti', config,
                    function () {
                        notificationService.displaySuccess("Xóa thành công");
                        search();
                    },
                    function (response) {
                        notificationService.displayError("Xóa không thành công");
                    });
            });
        }

        function selectAll() {

        }

        //Initial on load
        search();
    }
})(angular.module('webstore'));