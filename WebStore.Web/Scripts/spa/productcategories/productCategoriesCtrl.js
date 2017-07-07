/// <reference path="../../vendors/angular.js" />
(function (app) {
    'use strict';

    app.controller('productCategoriesCtrl', productCategoriesCtrl);

    productCategoriesCtrl.$inject = ['$scope', '$state', 'apiService', 'notificationService', '$ngBootbox'];

    function productCategoriesCtrl($scope, $state, apiService, notificationService, $ngBootbox) {
        $scope.productCategories = [];
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

        $scope.pagingInfo = {
            page: 1,
            itemsPerPage: 7,
            sortBy: 'ID',
            reverse: false,
            filterString: '',
            totalItems: 0
        };

        function search(page) {
            $scope.loadingState = true;

            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pageSize: $scope.pageSize,
                    filter: $scope.filterString
                }
            }

            apiService.get('api/productcategories/getall', config,
                getSuccess,
                getFailure
            );
        }

        function getSuccess(result) {
            $scope.productCategories = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
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

        $scope.sort = function (sortBy) {
            if (sortBy === $scope.pagingInfo.sortBy) {
                $scope.pagingInfo.reverse = !$scope.pagingInfo.reverse;
            } else {
                $scope.pagingInfo.sortBy = sortBy;
                $scope.pagingInfo.reverse = false;
            }
            $scope.pagingInfo.page = 1;
            //loadUsers();
            search();
        };

        $scope.selectPage = function (page) {
            $scope.pagingInfo.page = page;
            //loadUsers();
            search();
        };

        function onEdit(id) {
            $state.go("productcategory_edit", { id: id });
        }

        function onDelete(id) {
            $ngBootbox.confirm("Bạn có chắc chắn muốn xóa?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("api/productcategories/delete", config,
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
                angular.forEach($scope.productCategories, function (item) {
                    if (item.checked === true) {
                        checkedId.push(item.ID);
                    }
                });

                var config = {
                    params: {
                        checkedId: JSON.stringify(checkedId)
                    }
                }

                apiService.del("api/productcategories/deleteMulti", config,
                    function () {
                        notificationService.displaySuccess("Xóa thành công");
                        search();
                    },
                    function () {
                        notificationService.displayError("Xóa không thành công");
                    });
            });
        }

        $scope.ischeckedAll = false;
        function selectAll() {
            if ($scope.ischeckedAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.ischeckedAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.ischeckedAll = false;
            }
        }

        $scope.search();
    }

})(angular.module('webstore'));