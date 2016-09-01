namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _692016 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
        }
    }
}
