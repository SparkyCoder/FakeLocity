angular.module('fakelocity', [])
.service('EmployeeService', function ($http) {

        var urlBase = '/api/';

        function getAllEmployees(){
          return $http.get(urlBase+'GetAllEmployees').success(function(response, status){
            return response.data;
          });
        }

        function deleteEmployee(employee){
          return $http.post(urlBase+'deleteEmployee', employee).success(function(response, status){
            return status;
          });
        }

        function addEmployee(employee, dependents){

          var parameters = {
            NewEmployee: employee,
            dependents: dependents
          };

          return $http.post(urlBase+'AddEmployee', parameters).success(function(response, status){
            return status;
          });
        }

        function getPayrollDetails(employeeID, name){

          var parameters = {
            EmployeeID: employeeID,
            Name: name
          };

          return $http.post(urlBase+'GetPayrollDetails', parameters).success(function(response, status){
            return response.data;
          });
        }

        return{
          getAllEmployees: getAllEmployees,
          addEmployee: addEmployee,
          deleteEmployee: deleteEmployee,
          getPayrollDetails: getPayrollDetails
        };
    })
.directive('addEmployeeDirective', function () {
        return {
            scope: {
              employees: '='
            },
            templateUrl: 'Layouts/addEmployeeLayout.html',
            restrict: 'E',
            controller: function ($scope, EmployeeService) {
              $scope.employeeName = '';
              $scope.dependentName = '';
              $scope.showNewDependentForm = false;
              $scope.error = false;

              var dependents = [];

              $scope.addDependent = function(){
                if($scope.dependentName)
                {
                  $scope.dependentError = false;
                  $scope.showNewDependentForm = false;

                dependents.push({
                  Name: $scope.dependentName
                });

                $scope.dependentName = '';
              }
              else{
                $scope.dependentError = true;
              }
              };

              $scope.addNewEmployee = function(){

                if($scope.employeeName)
                {
                  $scope.employeeError = false;

                  var employee = {
                    Name: $scope.employeeName
                  };

                    EmployeeService.addEmployee(employee, dependents).then(function(){

                      EmployeeService.getAllEmployees().then(function (listOfEmployees){
                        $scope.employees = listOfEmployees.data;
                        $scope.employeeName = '';
                      });
                    });
                  }
                  else{
                    $scope.employeeError = true;
                  }
              };

            }
        }
    })
.directive('listDirective', function () {
        return {
            scope: {
              employees: '=',
              selectedemployee: '='
            },
            templateUrl: 'Layouts/listLayout.html',
            restrict: 'E',
            controller: function ($scope, EmployeeService) {

              $scope.selectEmployee = function(employee){
                $scope.selectedemployee = employee;
              }

              $scope.deleteEmployee = function(employeeToDelete){
                EmployeeService.deleteEmployee(employeeToDelete).then(function(){

                  EmployeeService.getAllEmployees().then(function (listOfEmployees){
                  $scope.employees = listOfEmployees.data;

                });
              });
            };

              EmployeeService.getAllEmployees().then(function (listOfEmployees){
                $scope.employees = listOfEmployees.data;
              });
    }}
  })
  .directive('payrollDetails', function () {
          return {
              scope: {
                employees : '=',
                selectedemployee:'='
              },
              templateUrl: 'Layouts/payrollDetails.html',
              restrict: 'E',
              controller: function ($scope, EmployeeService) {

                $scope.$watch('selectedemployee', function(){

                  if(typeof $scope.selectedemployee.ID !== "undefined"){
                    EmployeeService.getPayrollDetails($scope.selectedemployee.ID, $scope.selectedemployee.Name).then(function (details){
                      $scope.payrolldetails = details.data;
                  });
                }
                });
              }
      }
    })
.directive('fakelocityApp', function () {
    return {
        templateUrl: 'Layouts/masterlayout.html',
        restrict: 'E',
        controller: function ($scope, EmployeeService) {
          $scope.employees = [];
          $scope.selectedemployee;
        }
    };
})
