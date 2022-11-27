using FluentMigrator;

namespace SharedContext.Infrastructure.FM.Migrations
{
    [Migration(20221127001100)]
    public class CreatePeople : Migration
    {
        private const string PeopleTableName = "people";
        private const string SchemaName = "test_schema";

        public override void Down()
        {
            this.Delete.Table(PeopleTableName).InSchema(SchemaName);
            this.Delete.Schema(SchemaName);
        }

        public override void Up()
        {
            this.Create.Schema(SchemaName);

            this.Create
                .Table(PeopleTableName)
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable();
        }
    }
}
