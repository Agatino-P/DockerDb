using FluentMigrator;

namespace SharedContext.Infrastructure.FM.Migrations
{
    [Migration(20221127001100)]
    public class CreatePeople : Migration
    {
        private const string CompanyTableName = "People";
        private const string SchemaName = "TestSchema";

        public override void Down()
        {
            this.Delete.Table(CompanyTableName).InSchema(SchemaName);
            this.Delete.Schema(SchemaName);
        }

        public override void Up()
        {
            this.Create.Schema(SchemaName);

            this.Create
                .Table(CompanyTableName)
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable();
        }
    }
}
