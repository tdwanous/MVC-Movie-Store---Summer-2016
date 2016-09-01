namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testmigration2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.UserInfoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        userName = c.String(),
                        Email = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
