using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DapperInMVCCore.Models;
using Dapper;

namespace DapperInMVCCore.Controllers
{
    public class CourseController : Controller
    {
        //Create Database Connection
        string Connection = "Data Source=DESKTOP-RMRMV4N; Initial Catalog=Student; Integrated Security=True";

        public IActionResult Index()
        {
            return View();
        }

        // View Course Information
        public IActionResult ViewCourse()
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                var course = connection.Query<Course>("select  * from Course").ToList();
                connection.Close();
                return View(course);
            }
        }

        // Create Course
        [HttpGet] 
        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCourse(Course course)
        {
            if (course != null)
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    var command = "Insert Into Course (CourseName,CourseCredit) values ('" + course.CourseName + "','" + course.CourseCredit + "')";
                    connection.Execute(command);
                    connection.Close();
                    return RedirectToAction("ViewCourse");
                }
            }
            return View(course);
        }

        //Edit Teacher Information
        [HttpGet]
        public IActionResult EditCourse(int id) 
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                var editcourse = connection.QueryFirstOrDefault<Course>("select * from Course where CourseId=" + id + "");
                connection.Close();
                return View(editcourse);
            }
        }

        public IActionResult EditCourse(Course course)
        {
            if (course != null) 
            {
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    var editteacher = "Update Course set CourseName='" + course.CourseName + "',CourseCredit='" + course.CourseCredit + "' where CourseId=" + course.CourseId + ";";
                    connection.Execute(editteacher);
                    connection.Close();
                    return RedirectToAction("ViewCourse");
                }
            }
            return View(course);
        }

    }
}
