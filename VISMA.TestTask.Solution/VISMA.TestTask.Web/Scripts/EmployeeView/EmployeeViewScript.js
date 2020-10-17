
function ready(f) {
    /in/.test(document.readyState) ? setTimeout('ready(' + f + ')', 9) : f();
}

ready(function () {
    var vm = new Vue({
        el: '#VueModel',
        data: {
            alert: {
                errorMessage: null,
                showError: false,
                successMessage: null,
                showSuccess: false
            },
            employee: [],
            sortOrder: 1,
            sortColumn: "Id",
            pageNumber: 1,
            pageTotal: 0,
            endpoints
        },
        methods: {
            getEmployeeList: function () {
                $.post(vm.endpoints.getEmployeeListUrl, { pageNumber: vm.pageNumber - 1, orderValue: vm.sortColumn, sortOrder: vm.sortOrder }, function (response) {
                    if (response.StatusCode !== 200) {
                        vm.alert.errorMessage = response.ResponseMessage;
                        vm.alert.showError = true;
                    } else {
                        vm.employee = response.Data.Collection;
                        vm.pageTotal = response.Data.TotalPageCount;
                    }
                });
            },
            pageLeft: function () {
                if (vm.pageNumber > 1) {
                    vm.pageNumber--;
                    vm.getEmployeeList();
                }
            },
            pageRight: function () {
                if (vm.pageNumber !== vm.pageTotal) {
                    vm.pageNumber++;
                    vm.getEmployeeList();
                }
            },
            setPage: function (page) {
                if (page !== vm.pageNumber) {
                    vm.pageNumber = page;
                    vm.getEmployeeList();
                }
            },
            sort: function (sortColumn) {
                if (sortColumn === vm.sortColumn) {
                    if (vm.sortOrder === 0) {
                        vm.sortOrder = 1;
                    } else {
                        vm.sortOrder = 0;
                    }

                    vm.pageNumber = 1;
                    vm.getEmployeeList();
                } else {
                    vm.sortOrder = 0;
                    vm.sortColumn = sortColumn;
                    vm.pageNumber = 1;
                    vm.getEmployeeList();
                }
            },
            clearEmployeeModel: function () {
                modal.clearEmployeeModel();
            }
        },
        mounted: function () {
            this.$nextTick(function () {
                vm.getEmployeeList();
            });

            $("#employeeModal").modal({
                backdrop: 'static',
                show: false
            });

            $('#employeeModal').on('shown.bs.modal',
                function () {
                    $('#myInput').trigger('focus');
                });
        }
    });
    var modal = new Vue(
        {
            el: '#employeeModal',
            data: {
                newEmployee: {
                    firstName: null,
                    lastName: null,
                    socialSecurityNumber: null,
                    phoneNumber: null
                },
                alert: {
                    errorMessage: null,
                    showError: false
                },
                endpoints
            },
            methods: {
                addEmployee: function () {
                    // It is better to validate data on client before sending it to server, but for this small task I skip it.
                    $.post(vm.endpoints.addNewEmployerUrl, modal.newEmployee, function (response) {
                        if (response.StatusCode !== 200) {
                            modal.alert.errorMessage = response.ResponseMessage.split("\n");
                            modal.alert.showError = true;
                        } else {
                            $("#employeeModal").modal("hide");
                            vm.getEmployeeList();
                            vm.alert.successMessage = "New employee successfully added.";
                            vm.alert.showSuccess = true;
                        }
                    });
                },
                clearEmployeeModel: function () {
                    modal.newEmployee.firstName = null;
                    modal.newEmployee.lastName = null;
                    modal.newEmployee.socialSecurityNumber = null;
                    modal.newEmployee.phoneNumber = null;

                    modal.alert.errorMessage = null;
                    modal.alert.showError = false;
                }
            }
        }
    );
});