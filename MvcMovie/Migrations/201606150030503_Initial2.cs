namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Movies", "isFeatured", c => c.Boolean(nullable: false));
            AddColumn("dbo.Movies", "isDiscounted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Movies", "Discount");
            DropColumn("dbo.Movies", "Featured");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Featured", c => c.Boolean());
            AddColumn("dbo.Movies", "Discount", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Movies", "isDiscounted");
            DropColumn("dbo.Movies", "isFeatured");
            DropColumn("dbo.Movies", "DiscountPrice");
        }
    }
}
