namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TW6132016 : DbMigration
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
                        userID = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userID);
            
        }
    }
}
