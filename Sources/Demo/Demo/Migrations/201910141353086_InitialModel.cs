namespace Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Coin = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        CourseName = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseArtUrl = c.String(),
                        Description = c.String(),
                        Available = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Courses", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Courses", new[] { "CategoryId" });
            DropIndex("dbo.Courses", new[] { "AuthorId" });
            DropTable("dbo.Courses");
            DropTable("dbo.Categories");
            DropTable("dbo.Authors");
        }
    }
}
