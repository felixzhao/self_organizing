namespace SelfOrganization.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Project_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectDescription = c.String(),
                        Status = c.Boolean(nullable: false),
                        Creator_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.UserProfile", t => t.Creator_UserId)
                .Index(t => t.Creator_UserId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemDescription = c.String(),
                        Score = c.Int(nullable: false),
                        SelectID = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Select",
                c => new
                    {
                        SelectId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SelectId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Project", new[] { "Creator_UserId" });
            DropForeignKey("dbo.Project", "Creator_UserId", "dbo.UserProfile");
            DropTable("dbo.Select");
            DropTable("dbo.Member");
            DropTable("dbo.Item");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Project");
        }
    }
}
