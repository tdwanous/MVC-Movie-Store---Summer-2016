namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserInfos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserInfos",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userName);
            
        }
    }
}
