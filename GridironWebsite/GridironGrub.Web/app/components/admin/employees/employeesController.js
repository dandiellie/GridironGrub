(function () {

    angular
        .module('starter')
        .controller('employeesController', ['employeesService', 'loginService', employeesController]);

    function employeesController(employeesService, loginService) {
        var vm = this;
        vm.isLoading = false;
        vm.isLoading2 = true;
        vm.showNew = false;
        vm.showAll = true;
        vm.showFind = false;
        vm.showNewEmployeeDetails = false;
        vm.employee = '';
        vm.sortType = 'username'; // set the default sort type
        vm.sortReverse = false;  // set the default sort order
        vm.search = '';     // set the default search/filter term

        vm.goToNew = goToNew;
        vm.goToAll = goToAll;
        vm.goToFind = goToFind;
        vm.register = register;
        vm.newDetails = newDetails;
        vm.getEmployee = getEmployee;
        vm.getRestaurants = getRestaurants;
        vm.updateEmployee = updateEmployee;

        employeesService.getEmployees().then(successGetEmployees, failGetEmployees);

        function goToNew() {
            vm.showNew = true;
            vm.showAll = false;
            vm.showFind = false;
            employeesService.getRestaurants().then(successGetRestaurants, failGetRestaurants);
        }
        function goToAll() {
            vm.showNew = false;
            vm.showAll = true;
            vm.showFind = false;
            employeesService.getEmployees().then(successGetEmployees, failGetEmployees);
        }
        function goToFind(username) {
            vm.showNew = false;
            vm.showAll = false;
            vm.showFind = true;
            if (username) {
                employeesService.getEmployee(username).then(successGetEmployee, failGetEmployee);
            }
        }
        function register() {
            vm.isLoading = !vm.isLoading;
            loginService.register(vm.employee.username, vm.employee.password, vm.employee.confirmPassword).then(successRegister, failRegister);
        }
        function newDetails() {
            vm.isLoading2 = !vm.isLoading2;
            employeesService.postEmployee(vm.employee).then(successNewDetails, failNewDetails);
        }
        function getEmployee(username) {
            employeesService.getEmployee(username).then(successGetEmployee, failGetEmployee);
        }
        function getRestaurants() {
            employeesService.getRestaurants().then(successGetRestaurants, failGetRestaurants);
        }
        function updateEmployee() {
            vm.isLoading = true;
            employeesService.postEmployee(vm.employee).then(successUpdate, failUpdate);
        }

        function successGetEmployees(data) {
            vm.employees = data;
        }
        function failGetEmployees(data) {

        }

        function successGetRestaurants(data) {
            vm.restaurants = data;
        }
        function failGetRestaurants(data) {

        }

        function successRegister(data) {
            vm.showNewEmployeeDetails = true;
            vm.isLoading2 = false;
        }
        function failRegister(data) {
            vm.isLoading = !vm.isLoading;
            vm.employee = {};
        }

        function successNewDetails(data) {
            vm.isLoading = !vm.isLoading;
            vm.isLoading2 = true;
            vm.showNewEmployeeDetails = false;
            vm.employee = '';
            vm.goToAll();
        }
        function failNewDetails(data) {
            vm.isLoading = !vm.isLoading;
            vm.isLoading2 = !vm.isLoading2;
            vm.employee = {};
        }
        
        function successGetEmployee(data) {
            vm.employee = data;
        }
        function failGetEmployee(data) {
            
        }

        function successUpdate(data) {
            vm.isLoading = !vm.isLoading;
            vm.goToFind(vm.employee.username);
        }
        function failUpdate(data) {
            vm.isLoading = !vm.isLoading;
            vm.employee = '';
        }

    }
})();