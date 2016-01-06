namespace webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c115 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        PersonId = c.Int(nullable: false),
                        wocao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.PhotoPrices", t => t.wocao)
                .Index(t => t.PersonId)
                .Index(t => t.wocao);
            
            CreateTable(
                "dbo.PhotoPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "wocao", "dbo.PhotoPrices");
            DropForeignKey("dbo.Photos", "PersonId", "dbo.Person");
            DropIndex("dbo.Photos", new[] { "wocao" });
            DropIndex("dbo.Photos", new[] { "PersonId" });
            DropTable("dbo.PhotoPrices");
            DropTable("dbo.Photos");
            DropTable("dbo.Person");
        }
    }
}
