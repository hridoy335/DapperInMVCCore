using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperInMVCCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperInMVCCore.Controllers
{
    public class TeacherController : Controller
    {
        //Create Database Connection
        string Connection = "Data Source=DESKTOP-RMRMV4N; Initial Catalog=Student; Integrated Security=True";
        public IActionResult Index()
        {
            return View();
        }

        // View Teacher Information
        public IActionResult ViewTeacher()
        {
            using (SqlConnection connection = new SqlConnection(Connection))
            {
                connection.Open();
                var teacher = connection.Query<Teacher>("select  * from Teacher").ToList();
                connection.Close();
                return View(teacher); 
            }
        }

        // Create Teacher
        [HttpGet]
        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            if (teacher!=null)
            {
                using (SqlConnection connection=new SqlConnection(Connection))
                {
                    connection.Open();
                    var command = "Insert Into Teacher (TeacherName,TeacherEmail,TeacherPhone) values ('"+teacher.TeacherName+"','"+teacher.TeacherEmail+"','"+teacher.TeacherPhone+"')";
                    connection.Execute(command);
                    connection.Close();
                    return RedirectToAction("ViewTeacher");
                }
            }
            return View(teacher);
        }

        //Edit Teacher Information
        [HttpGet]
        public IActionResult EditTeacher(int id)
        {
            using (SqlConnection connection=new SqlConnection(Connection))
            {
                connection.Open();
                var editteacher = connection.QueryFirstOrDefault<Teacher>("select * from Teacher where TeacherId=" + id + "");
                connection.Close();
                return View(editteacher);
            } 
        }

        public IActionResult EditTeacher(Teacher teacher)
        {
            if (teacher!=null)
            {
                using (SqlConnection connection=new SqlConnection(Connection))
                {
                    connection.Open();
                    var editteacher= "Update Teacher set TeacherName='" + teacher.TeacherName + "',TeacherEmail='" + teacher.TeacherEmail + "',TeacherPhone='" + teacher.TeacherPhone + "' where TeacherId=" + teacher.TeacherId + ";";
                    connection.Execute(editteacher);
                    connection.Close();
                    return RedirectToAction("ViewTeacher");
                }
            }
            return View(teacher);
        }




    }
}
