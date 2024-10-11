namespace ARM_Vyz.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.__MigrationHistory",
                c => new
                    {
                        MigrationId = c.String(nullable: false, maxLength: 150),
                        ContextKey = c.String(nullable: false, maxLength: 300),
                        Model = c.Binary(nullable: false),
                        ProductVersion = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.MigrationId, t.ContextKey });
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        FacultyID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Faculties", t => t.FacultyID)
                .Index(t => t.FacultyID);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        FacultyID = c.Int(nullable: false, identity: true),
                        FacultyName = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        DeanID = c.Int(),
                    })
                .PrimaryKey(t => t.FacultyID)
                .ForeignKey("dbo.People", t => t.DeanID)
                .Index(t => t.DeanID);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PeopleID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(),
                        FIO = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Birthday = c.DateTime(nullable: false, storeType: "date"),
                        HaveAChild = c.Boolean(nullable: false),
                        Scholarship = c.Decimal(storeType: "money"),
                        Gender = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Salary = c.Decimal(storeType: "money"),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PeopleID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.RoleID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.ScientificDegrees",
                c => new
                    {
                        ScientificDegreeID = c.Int(nullable: false, identity: true),
                        NameDissertation = c.String(maxLength: 150, fixedLength: true),
                        Year = c.DateTime(storeType: "date"),
                        TeacherID = c.Int(),
                    })
                .PrimaryKey(t => t.ScientificDegreeID)
                .ForeignKey("dbo.People", t => t.TeacherID)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.ScientificDegrees", "TeacherID", "dbo.People");
            DropForeignKey("dbo.People", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Faculties", "DeanID", "dbo.People");
            DropForeignKey("dbo.Departments", "FacultyID", "dbo.Faculties");
            DropIndex("dbo.ScientificDegrees", new[] { "TeacherID" });
            DropIndex("dbo.People", new[] { "DepartmentID" });
            DropIndex("dbo.People", new[] { "RoleID" });
            DropIndex("dbo.Faculties", new[] { "DeanID" });
            DropIndex("dbo.Departments", new[] { "FacultyID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.ScientificDegrees");
            DropTable("dbo.Roles");
            DropTable("dbo.People");
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
            DropTable("dbo.__MigrationHistory");
        }
    }
}
