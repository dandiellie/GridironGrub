(function () {

    angular
        .module('starter')
        .controller('parkController', ['parkService', parkController]);

    function parkController(parkService) {
        var vm = this;
        vm.showSeats = false;
        vm.showVendors = true;
        vm.showSpecific = false;
        vm.isLoading = false;
        vm.sortType = 'section'; // set the default sort type
        vm.sortReverse = false;  // set the default sort order
        vm.vendor = '';
        vm.seat = {};
        vm.parkToUpdate = {};
        vm.areaToSave = {};
        vm.vendorToSave = {};
        vm.categoryToSave = {};
        vm.itemToSave = {};

        vm.goToSeats = goToSeats;
        vm.goToVendors = goToVendors;
        vm.goToSpecific = goToSpecific;
        vm.getSpecific = getSpecific;
        vm.setSeat = setSeat;
        vm.postSeat = postSeat;
        vm.setPark = setPark;
        vm.setArea = setArea;
        vm.setVendor = setVendor;
        vm.setCategory = setCategory;
        vm.setItem = setItem;
        vm.updatePark = updatePark;
        vm.saveArea = saveArea;
        vm.deleteArea = deleteArea;
        vm.saveVendor = saveVendor;
        vm.deleteVendor = deleteVendor;
        vm.saveCategory = saveCategory;
        vm.deleteCategory = deleteCategory;
        vm.saveItem = saveItem;
        vm.deleteItem = deleteItem;

        parkService.getPark().then(successGetVendors, failGetVendors);

        function goToSeats() {
            vm.showSeats = true;
            vm.showVendors = false;
            vm.showSpecific = false;
            parkService.getSeats().then(successGetSeats, failGetSeats);
        }
        function goToVendors() {
            vm.showSeats = false;
            vm.showVendors = true;
            vm.showSpecific = false;
            parkService.getPark().then(successGetVendors, failGetVendors);
        }
        function goToSpecific(restId) {
            vm.showSeats = false;
            vm.showVendors = false;
            vm.showSpecific = true;
            if (restId) {
                vm.getSpecific(restId);
            }
        }
        function getSpecific(restId) {
            parkService.getVendor(restId).then(successGetVendor, failGetVendor);
        }
        function setSeat(areaId, areaName, seatId, seatSection, seatRow, seatChair) {
            vm.seat.areaId = areaId;
            vm.seat.area = areaName;
            vm.seat.id = seatId;
            vm.seat.section = seatSection;
            vm.seat.row = seatRow;
            vm.seat.chair = seatChair;
        }
        function postSeat() {
            vm.isLoading = true;
            parkService.postSeat(vm.seat).then(successPostSeat, failPostSeat);
        }
        function setPark() {
            vm.parkToUpdate.name = vm.park.name;
            vm.parkToUpdate.logoUrl = vm.park.logoUrl;
            vm.parkToUpdate.taxRate = vm.park.taxRate;
        }
        function setArea(areaName, areaId) {
            vm.areaToSave.id = areaId ? areaId : undefined;
            vm.areaToSave.name = areaName;
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
        function updatePark() {
            vm.isLoading = true;
            parkService.updatePark(vm.parkToUpdate).then(successUpdatePark, failUpdatePark);
        }
        function saveArea() {
            vm.isLoading = true;
            parkService.saveArea(vm.areaToSave).then(successSaveArea, failSaveArea);
        }
        function deleteArea() {
            vm.isLoading = true;
            vm.areaToSave.isRetired = true;
            parkService.saveArea(vm.areaToSave).then(successSaveArea, failSaveArea);
        }
        function saveVendor() {
            vm.isLoading = true;
            parkService.saveVendor(vm.vendorToSave).then(successSaveVendor, failSaveVendor);
        }
        function deleteVendor() {
            vm.isLoading = true;
            vm.vendorToSave.isRetired = true;
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

        function successGetSeats(data) {
            vm.parkSeat = data;
        }
        function failGetSeats(data) {

        }

        function successPostSeat(data) {
            vm.isLoading = false;
            vm.seat = {};
            vm.goToSeats();
        }
        function failPostSeat(data) {
            vm.isLoading = false;
            vm.seat = {};
        }

        function successUpdatePark(data) {
            vm.isLoading = false;
            vm.parkToUpdate = {};
            parkService.getPark().then(successGetVendors, failGetVendors);
            parkService.getSeats().then(successGetSeats, failGetSeats);
        }
        function failUpdatePark(data) {
            vm.isLoading = false;
            vm.parkToUpdate = {};
        }

        function successSaveArea(data) {
            vm.isLoading = false;
            vm.areaToSave = {};
            parkService.getPark().then(successGetVendors, failGetVendors);
            parkService.getSeats().then(successGetSeats, failGetSeats);
        }
        function failSaveArea(data) {
            vm.isLoading = false;
            vm.areaToSave = {};
        }

        function successSaveVendor(data) {
            vm.isLoading = false;
            vm.vendorToSave = {};
            vm.goToVendors();
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