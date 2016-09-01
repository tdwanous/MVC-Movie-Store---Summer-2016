namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testupdate : DbMigration
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
                        userID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userID);
            
        }
    }
}
