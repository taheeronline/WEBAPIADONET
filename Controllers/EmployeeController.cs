using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        SqlDataReader reader = null;
        SqlConnection myConnection = new SqlConnection();

        public EmployeeController()
        {
            myConnection.ConnectionString = @"Server=IE-0015;Database=Test_EmployeeDetails;Trusted_Connection=True;";
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Employee_Details";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            List<Employee> empList = new List<Employee>();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.Employee_Id = Convert.ToInt32(reader.GetValue(0));
                emp.Employee_Name = reader.GetValue(1).ToString();
                emp.Employee_Salary = Convert.ToInt32(reader.GetValue(2));
                empList.Add(emp);
            }
            myConnection.Close();
            return empList;
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public Employee Get(int id)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Employee_Details where Employee_Id=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.Employee_Id = Convert.ToInt32(reader.GetValue(0));
                emp.Employee_Name = reader.GetValue(1).ToString();
                emp.Employee_Salary = Convert.ToInt32(reader.GetValue(2));
            }
            myConnection.Close();
            return emp;
        }

        [HttpPost]
        public void AddEmployee(Employee employee)
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Employee_Details (Employee_Name,Employee_Salary) Values (@Employee_Name,@Employee_Salary)";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@Employee_Name", employee.Employee_Name);
            sqlCmd.Parameters.AddWithValue("@Employee_Salary", employee.Employee_Salary);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
