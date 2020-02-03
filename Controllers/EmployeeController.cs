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
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_InsertEmployee";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@EName", employee.Employee_Name);
            sqlCmd.Parameters.AddWithValue("@ESalary", employee.Employee_Salary);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        [HttpPut(Name = "Put")]
        public void PutEmployee(Employee employee)
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_UpdateEmployee";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@EName", employee.Employee_Name);
            sqlCmd.Parameters.AddWithValue("@ESalary", employee.Employee_Salary);
            sqlCmd.Parameters.AddWithValue("@ID", employee.Employee_Id);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        [HttpDelete("{ID}", Name = "Delete")]
        public void DeleteEmployee(int ID)
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "sp_DeleteEmployee";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@ID", ID);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

    }
}
