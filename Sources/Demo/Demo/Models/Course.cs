using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CourseName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string CourseArtUrl { get; set; }

        public string Description { get; set; }

        [DefaultValue(true)]
        public bool Available { get; set; }

        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime CreateDate { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }

    public class GetCourse
    {
        public int CourseId { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }

        public string CourseName { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public string CourseArtUrl { get; set; }

        public string Description { get; set; }
    }
}