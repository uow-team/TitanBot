namespace TitanBotX.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guilds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Guild_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Guilds", t => t.Guild_ID)
                .Index(t => t.Guild_ID);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Points = c.Int(nullable: false),
                        DiscordId = c.String(),
                        Rank_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ranks", t => t.Rank_ID)
                .Index(t => t.Rank_ID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Rank_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ranks", t => t.Rank_ID)
                .Index(t => t.Rank_ID);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Number = c.Int(nullable: false),
                        Guild_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Guilds", t => t.Guild_ID)
                .Index(t => t.Guild_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rules", "Guild_ID", "dbo.Guilds");
            DropForeignKey("dbo.Ranks", "Guild_ID", "dbo.Guilds");
            DropForeignKey("dbo.Permissions", "Rank_ID", "dbo.Ranks");
            DropForeignKey("dbo.Members", "Rank_ID", "dbo.Ranks");
            DropIndex("dbo.Rules", new[] { "Guild_ID" });
            DropIndex("dbo.Permissions", new[] { "Rank_ID" });
            DropIndex("dbo.Members", new[] { "Rank_ID" });
            DropIndex("dbo.Ranks", new[] { "Guild_ID" });
            DropTable("dbo.Rules");
            DropTable("dbo.Permissions");
            DropTable("dbo.Members");
            DropTable("dbo.Ranks");
            DropTable("dbo.Guilds");
        }
    }
}
