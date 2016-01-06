(function () {

    angular
        .module('starter')
        .controller('managerController', ['managerService', 'parkService', managerController]);

    function managerController(managerService, parkService) {
        var vm = this;
        vm.showAll = true;
        vm.showSpecific = false;
        vm.vendorToSave = {};
        vm.categoryToSave = {};
        vm.itemToSave = {};

        vm.goToAll = goToAll;
        vm.goToSpecific = goToSpecific;
        vm.setVendor = setVendor;
        vm.setCategory = setCategory;
        vm.setItem = setItem;
        vm.saveVendor = saveVendor;
        vm.saveCategory = saveCategory;
        vm.deleteCategory = deleteCategory;
        vm.saveItem = saveItem;
        vm.deleteItem = deleteItem;

        managerService.getVendors().then(successGetVendors, failGetVendors);

        function goToAll() {
            vm.showAll = true;
            vm.showSpecific = false;
            managerService.getVendors().then(successGetVendors, failGetVendors);
        }
        function goToSpecific(vendorId) {
            vm.showAll = false;
            vm.showSpecific = true;
            if (vendorId) {
                parkService.getVendor(vendorId).then(successGetVendor, failGetVendor);
            }
        }
        function setVendor(areaId, id, name, description) {
            vm.vendorToSave.areaId = areaId;
            vm.vendorToSave.id = id ? id : undefined;
            vm.vendorToSave.name = name ? name : undefined;
            vm.vendorToSave.description = description ? description : undefined;
        }
        function setCategory(vendorId, categoryName, categoryId) {
            vm.categoryToSave.vendorId = vendorId;
            vm.categoryToSave.id = categoryId ? categoryId : undefined;
            vm.categoryToSave.name = categoryName ? categoryName : undefined;
        }
        function setItem(categoryId, itemId, itemName, imageUrl, price, isAlcohol) {
            vm.itemToSave.categoryId = categoryId;
            vm.itemToSave.id = itemId;
            vm.itemToSave.name = itemName;
            vm.itemToSave.imageUrl = imageUrl;
            vm.itemToSave.price = price;
            vm.itemToSave.isAlcohol = isAlcohol;
        }
        function saveVendor() {
            vm.isLoading = true;
            parkService.saveVendor(vm.vendorToSave).then(successSaveVendor, failSaveVendor);
        }
        function saveCategory() {
            vm.isLoading = true;
            parkService.saveCategory(vm.categoryToSave).then(successSaveCategory, failSaveCategory);
        }
        function deleteCategory() {
            vm.isLoading = true;
            vm.categoryToSave.isRetired = true;
            parkService.saveCategory(vm.categoryToSave).then(successSaveCategory, failSaveCategory);
        }
        function saveItem() {
            vm.isLoading = true;
            parkService.saveItem(vm.itemToSave).then(successSaveItem, failSaveItem);
        }
        function deleteItem() {
            vm.isLoading = true;
            vm.itemToSave.isRetired = true;
            parkService.saveItem(vm.itemToSave).then(successSaveItem, failSaveItem);
        }

        function successGetVendors(data) {
            vm.park = data;
        }
        function failGetVendors(data) {

        }

        function successGetVendor(data) {
            vm.vendor = data;
        }
        function failGetVendor(data) {

        }

        function successSaveVendor(data) {
            vm.isLoading = false;
            vm.vendorToSave = {};
            vm.goToSpecific(vm.vendor.id);
        }
        function failSaveVendor(data) {
            vm.isLoading = false;
            vm.vendorToSave = {};
        }

        function successSaveCategory(data) {
            vm.isLoading = false;
            vm.categoryToSave = {};
            vm.goToSpecific(vm.vendor.id);
        }
        function failSaveCategory(data) {
            vm.isLoading = false;
            vm.categoryToSave = {};
        }

        function successSaveItem(data) {
            vm.isLoading = false;
            vm.itemToSave = {};
            vm.goToSpecific(vm.vendor.id);
        }
        function failSaveItem(data) {
            vm.isLoading = false;
            vm.itemToSave = {};
        }

    }
})();