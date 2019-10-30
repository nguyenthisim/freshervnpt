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
using System.Web.Http.Results;
using System.Web.Mvc;
using Demo.Models;

namespace Demo.Controllers
{
    public class CategoriesController : ApiController
    {
        private APIContext db = new APIContext();

        // GET: api/Categories
        public HttpResponseMessage GetCategory_sp()
        {
            var categories = db.Categories.SqlQuery("sp_GetCategory").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, categories);
        }


        //[dbo].[DSCOURSE]
        //[System.Web.Http.Route("api/thongke")]
        //public string Post()
        //{
        //    var ten = "TOIEC";
        //    try
        //    {
        //        DataTable dataTable = new DataTable();
        //        string query = @"select * dbo.DSCOURSE ('" + ten + @"')";
        //        using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoAPIContext"].ConnectionString))
        //        using (var cmd = new SqlCommand(query, con))
        //        using (var da = new SqlDataAdapter(cmd))
        //        {
        //            cmd.CommandType = CommandType.Text;
        //            da.Fill(dataTable);
        //        }
        //        return "Successfully";
        //    }
        //    catch (Exception)
        //    {
        //        return "Failed";
        //    }
        //}

        // GET: api/Categories/5
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        public string PutCategory(Category category)
        {
            try
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return "Successfully";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        // POST: api/Categories

        public string Post(Category category)
        {
            try
            {
                db.Database.ExecuteSqlCommand("EXEC sp_InsertCategory @Title, @Active", new SqlParameter("@Title", category.Title), new SqlParameter("@Active", category.Active));
                return "Successfully";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }


        // DELETE: api/Categories/5
        [System.Web.Http.HttpDelete]
        public string Delete(int id)
        {
            try
            {
                Category category = db.Categories.SingleOrDefault(x => x.CategoryId == id);
                if (category != null)
                {
                    db.Categories.Remove(category);
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