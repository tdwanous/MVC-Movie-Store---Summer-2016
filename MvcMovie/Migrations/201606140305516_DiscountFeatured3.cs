namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountFeatured3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "Discount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
