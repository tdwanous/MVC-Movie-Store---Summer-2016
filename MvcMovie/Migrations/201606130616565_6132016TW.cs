namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6132016TW : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "posterLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "posterLink");
        }
    }
}
