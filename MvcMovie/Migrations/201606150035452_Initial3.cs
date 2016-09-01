namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "DiscountPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Movies", "isFeatured", c => c.Boolean());
            AlterColumn("dbo.Movies", "isDiscounted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "isDiscounted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Movies", "isFeatured", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Movies", "DiscountPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
