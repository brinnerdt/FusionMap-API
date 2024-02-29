using Dapper;
using FusionMapAPI.Data;
using FusionMapAPI.Dtos;
using FusionMapAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FusionMapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController(IConfiguration config) : ControllerBase
    {
        DataContextDapper _dapper = new DataContextDapper(config);

        [HttpGet("GetEmployees")]
        public IEnumerable<Employee> GetEmployees()
        {
            string sql = @"SELECT * FROM employee ORDER BY FirstName ASC";
            IEnumerable<Employee> employees = _dapper.LoadData<Employee>(sql);
            return employees;
        }

        [HttpGet("GetEmployees/{employeeId}")]
        public Employee GetSingleEmployee(int employeeId)
        {
            string sql = @"SELECT * FROM employee WHERE EmployeeId = @employeeid";
            var parameters = new DynamicParameters();
            parameters.Add("employeeid", employeeId);
            Employee employee = _dapper.LoadDataSingle<Employee>(sql, parameters);
            return employee;
        }
        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee(Employee employee)
        {
            string sql = @"
                UPDATE employee 
                    SET FirstName = @FirstName, 
                    LastName = @LastName, 
                    DOB = @DOB, 
                    Department = @Department, 
                    Salary = @Salary, 
                    Email = @Email, 
                    PhoneNumber = @PhoneNumber 
                WHERE EmployeeId = @EmployeeId";

            var parameters = new DynamicParameters();
            parameters.Add("EmployeeId", employee.EmployeeId);
            parameters.Add("FirstName", employee.FirstName);
            parameters.Add("LastName", employee.LastName);
            parameters.Add("DOB", employee.DOB);
            parameters.Add("Department", employee.Department);
            parameters.Add("Salary", employee.Salary);
            parameters.Add("Email", employee.Email);
            parameters.Add("PhoneNumber", employee.PhoneNumber);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee(EmployeeDto employee)
        {
            string sql = @"
                INSERT INTO employee (
                    FirstName, 
                    LastName, 
                    DOB, 
                    Department, 
                    Salary, 
                    Email, 
                    PhoneNumber
                ) VALUES (
                    @FirstName, 
                    @LastName, 
                    @DOB, 
                    @Department, 
                    @Salary, 
                    @Email, 
                    @PhoneNumber
                )";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName);
            parameters.Add("LastName", employee.LastName);
            parameters.Add("DOB", employee.DOB);
            parameters.Add("Department", employee.Department);
            parameters.Add("Salary", employee.Salary);
            parameters.Add("Email", employee.Email);
            parameters.Add("PhoneNumber", employee.PhoneNumber);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteEmployee/{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            string sql = @"DELETE FROM employee WHERE EmployeeId = @employeeid";
            var parameters = new DynamicParameters();
            parameters.Add("employeeid", employeeId);
            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }

}


