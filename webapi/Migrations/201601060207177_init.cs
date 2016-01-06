namespace webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Person2Photo", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Person2Photo", "PhotoId", "dbo.Photos");
            DropIndex("dbo.Person2Photo", new[] { "PersonId" });
            DropIndex("dbo.Person2Photo", new[] { "PhotoId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Person2Photo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Person2Photo",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PersonId, t.PhotoId });
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Person2Photo", "PhotoId");
            CreateIndex("dbo.Person2Photo", "PersonId");
            AddForeignKey("dbo.Person2Photo", "PhotoId", "dbo.Photos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Person2Photo", "PersonId", "dbo.Person", "Id", cascadeDelete: true);
        }
    }
}
