namespace WebStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContent1FieldOnSlide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slides", "Content1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Slides", "Content1");
        }
    }
}
