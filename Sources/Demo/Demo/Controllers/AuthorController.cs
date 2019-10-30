using Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Demo.Controllers
{
    public class AuthorController : ApiController
    {
        private APIContext db = new APIContext();
        // GET: api/Author
        public IEnumerable<Author> Get()
        {
            return db.Authors.ToList();
        }
        // GET: api/Author/5
        public HttpResponseMessage Get(int id)
        {
            Author model = db.Authors.Where(x => x.AuthorId == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Request.CreateResponse(HttpStatusCode.OK, value);
        }
        // POST: api/Author
        [HttpPost]
        public string Post(Author author)
        {
            try
            {
                Author findExist = db.Authors.Where(x => x.Email == author.Email).SingleOrDefault();
                if(findExist== null)
                {
                    author.DateCreated = DateTime.Now;
                    author.Coin = 0;
                    db.Authors.Add(author);
                    db.SaveChanges();
                    return "Successfully";
                }
                return "Author was exist!";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        // PUT: api/Author/5
        [HttpPut]
        public string Put(Author author)
        {
            try
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return "Successfully";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        // DELETE: api/Author/5
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
