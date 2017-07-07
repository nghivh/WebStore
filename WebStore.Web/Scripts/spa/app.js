/// <reference path="../vendors/angular.js" />

(function () {
    'use strict';

    angular.module('webstore', ['common.core', 'common.ui'])
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];

    function config($stateProvider, $urlRouterProvider, $locationProvider) {
        $urlRouterProvider.otherwise('/');

        $stateProvider
        .state('/', {
            url: "/admin",
            templateUrl: "/scripts/spa/home/index.html",
            controller: "indexCtrl"
        })
        .state('home', {
            url: "/admin",
            templateUrl: "/scripts/spa/home/index.html",
            controller: "indexCtrl"
        })
        .state('productcategories', {
            url: "/productcategories",
            templateUrl: "/scripts/spa/productcategories/productCategories.html",
            controller: "productCategoriesCtrl"
        })
        .state('productcategory_add', {
            url: "/productcategory_add",
            templateUrl: "/scripts/spa/productcategories/productCategoryAdd.html",
            controller: "productCategoryAddCtrl"
        })
        .state('productcategory_edit', {
            url: "/productcategory_edit/:id",
            templateUrl: "/scripts/spa/productcategories/productCategoryEdit.html",
            controller: "productCategoryEditCtrl"
        })

        .state('products', {
            url: "/products",
            templateUrl: "/scripts/spa/products/products.html",
            controller: "productsCtrl"
        })
        .state('product_add', {
            url: "/product_add",
            templateUrl: "/scripts/spa/products/productAdd.html",
            controller: "productAddCtrl"
        })
        .state('product_edit', {
            url: "/product_edit/:id",
            templateUrl: "/scripts/spa/products/productEdit.html",
            controller: "productEditCtrl"
        })
        //Thiết lập đường dẫn gọn hơn, bỏ qua #! trên đường dẫn
        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});
        //$locationProvider.hashPrefix('!');
    }
})();