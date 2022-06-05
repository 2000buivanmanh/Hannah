namespace DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BangViDu",
                c => new
                    {
                        KhoaChinh = c.String(nullable: false, maxLength: 128),
                        ThuocTinh = c.String(),
                    })
                .PrimaryKey(t => t.KhoaChinh);
            Sql("INSERT INTO BangViDu Values('Khóa 1','Thuộc tính 1')");
            Sql("INSERT INTO BangViDu Values('Khóa 2','Thuộc tính 2')");
            Sql("INSERT INTO BangViDu Values('Khóa xxx','Thuộc tính xxx')");
        }
        
        public override void Down()
        {
            DropTable("dbo.BangViDu");
        }
    }
}
