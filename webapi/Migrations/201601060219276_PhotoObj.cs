namespace webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoObj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "PersonId", "dbo.Person");
            DropIndex("dbo.Photos", new[] { "PersonId" });
            DropTable("dbo.Photos");
        }
    }
}
