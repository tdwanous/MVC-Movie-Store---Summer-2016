namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurfaceUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "orderStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "orderStatus");
        }
    }
}
