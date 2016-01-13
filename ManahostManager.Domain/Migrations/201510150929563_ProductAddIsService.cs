namespace ManahostManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAddIsService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsService", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsService");
        }
    }
}
