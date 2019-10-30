using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Demo.Models;

namespace Demo.Controllers
{
    public class CoursesController : ApiController
    {
        private APIContext db = new APIContext();

        //public HttpResponseMessage GetCourse_sp()
        //{
        //    var courses = db.Database.SqlQuery<GetCourse>("sp_GetCourse").ToList();
        //    return Request.CreateResponse(HttpStatusCode.OK, courses);
        //}

        // GET: api/Courses
        public IEnumerable<GetCourse> Get()
        {
            return db.Courses.Select(x => new GetCourse
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                Category = x.Category.Title,
                Author = x.Author.Name,
                Price = x.Price,
                Discount = x.Discount,
                CourseArtUrl=x.CourseArtUrl
            }).ToList(); 
        }

        public HttpResponseMessage Get(int id)
        {
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Courses where CourseId='"+id+@"'";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoAPIContext"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(dataTable);
            }
            return Request.CreateResponse(HttpStatusCode.OK, dataTable);
        }
     
        [Route("api/course-mana")]
        public IEnumerable<Course> GetList()
        {
            return db.Courses.ToList();
        }

        // PUT: api/Courses/5
        [HttpPut]
        public string Put(Course course)
        {
            try
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return "Successfully";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        // POST: api/Courses
        [HttpPost]
        public string Post(Course course)
        {
            try
            {
                    course.Available =true;
                    course.DateCreated = DateTime.Now;
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return "Successfully";
                
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        // DELETE: api/Courses/5
      
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
                Author author = db.Authors.SingleOrDefault(x => x.AuthorId == id);
                if (author != null)
                {
                    db.Authors.Remove(author);
                    db.SaveChanges();
                    return "Successfully";
                }
                return "Failed";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

    }
}