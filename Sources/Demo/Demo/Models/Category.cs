using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [DefaultValue(true)]
        public bool Active { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }
    }
}