namespace NewsService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        authorId = c.Int(nullable: false, identity: true),
                        password = c.String(),
                        authorName = c.String(),
                        authorImage = c.String(),
                        authorCity = c.String(),
                    })
                .PrimaryKey(t => t.authorId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        newsId = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        description = c.String(),
                        image = c.String(),
                        tag = c.String(),
                        newsCity = c.String(),
                        datetime = c.DateTime(nullable: false),
                        author_authorId = c.Int(),
                    })
                .PrimaryKey(t => t.newsId)
                .ForeignKey("dbo.Authors", t => t.author_authorId)
                .Index(t => t.author_authorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "author_authorId", "dbo.Authors");
            DropIndex("dbo.News", new[] { "author_authorId" });
            DropTable("dbo.News");
            DropTable("dbo.Authors");
        }
    }
}
