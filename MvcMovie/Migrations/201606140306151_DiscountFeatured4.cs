namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountFeatured4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Featured", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Featured", c => c.Boolean(nullable: false));
        }
    }
}
