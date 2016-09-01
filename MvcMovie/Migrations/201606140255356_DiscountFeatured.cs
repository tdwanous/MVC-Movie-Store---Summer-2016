namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Movies", "Featured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Featured");
            DropColumn("dbo.Movies", "Discount");
        }
    }
}
