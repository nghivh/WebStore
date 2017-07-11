namespace WebStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveContent1FieldOnSlide : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Slides", "Content1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Slides", "Content1", c => c.String());
        }
    }
}
