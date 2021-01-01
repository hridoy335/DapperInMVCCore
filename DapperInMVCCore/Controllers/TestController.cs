using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperInMVCCore.Models;
using System.Data.SqlClient;
using Dapper;

namespace DapperInMVCCore.Controllers
{
    public class TestController : Controller
    {
        //Create Database Connection
        string Connection = "Data Source=DESKTOP-RMRMV4N; Initial Catalog=Student; Integrated Security=True";
        public IActionResult Index()
        {
            return View();
        }

        // Add Student Information 
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(StudentInfo studentInfo)
        {
            if (studentInfo!=null)
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    string command = "Insert into StudentInfo (StudentName,Role,Address) values ('" + studentInfo.StudentName + "','" + studentInfo.Role + "','" + studentInfo.Address + "')";
                    connection.Execute(command);
                    connection.Close();
                    return RedirectToAction("");
                }
            }
         
            return View();
        }


        // View Student Information
        public IActionResult ViewStudent()
        {
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                connection.Open();
                var studentinfo = connection.Query<StudentInfo>("select  * from StudentInfo").ToList();
                connection.Close();
                return View(studentinfo);
            }
        }

        //Edit Student Info
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                var studentinfo = connection.QueryFirstOrDefault<StudentInfo>("select * from StudentInfo where StudentId=" + id + "");
                connection.Close();
                return View(studentinfo);
            }

        }

        [HttpPost]
        public IActionResult EditStudent(StudentInfo studentInfo)
        {
            if (studentInfo!=null)
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    var studentinfo = "Update studentInfo set StudentName='" + studentInfo.StudentName + "',Role='" + studentInfo.Role + "',Address='" + studentInfo.Address + "' where StudentId=" + studentInfo.StudentId + ";";
                    connection.Execute(studentinfo);
                    connection.Close();
                    return RedirectToAction("ViewStudent");
                }
            }
            return View(studentInfo);
        }

        //Delete Student Information
        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                var studentinfo = connection.QueryFirstOrDefault<StudentInfo>("select * from StudentInfo where StudentId=" + id + "");
                connection.Close();
                return View(studentinfo);
            }
        }

        [HttpPost]
        public IActionResult DeleteStudent(int?id)
        {
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    var studentinfo = "Delete from StudentInfo where StudentId=" +id+ ";";
                    connection.Execute(studentinfo);
                    connection.Close();
                    return RedirectToAction("ViewStudent");
                }
            }
            return View();
            
        }


    }
}
