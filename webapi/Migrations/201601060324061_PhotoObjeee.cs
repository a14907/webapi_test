namespace webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhotoObjeee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhotoPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        PhotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Photos", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoPrices", "PhotoId", "dbo.Photos");
            DropIndex("dbo.PhotoPrices", new[] { "PhotoId" });
            DropTable("dbo.PhotoPrices");
        }
    }
}
