using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using System.Linq.Expressions;

namespace Demo.Models
{
    public class APIContext : DbContext
    {
        public APIContext() : base("DemoAPIContext")
        {
        }
       
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
    }
}