using FluentMigrator;

namespace Lesson_8.Data
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            CreateTable("employees");
        }

        public override void Down()
        {
            Delete.Table("employees");
        }

        private void CreateTable(string tableName)
        {
            Create.Table(tableName)
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Position").AsString()
                .WithColumn("Age").AsInt64()
                .WithColumn("CreatedDate").AsInt64();
        }
    }
}
