using FluentMigrator;

namespace InteractiveSeven.UI.Migrations
{
    [Migration(1)]
    public class Migration0001AddSettingsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Settings")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString().Unique().NotNullable()
                .WithColumn("Command").AsString().Unique().Nullable()
                .WithColumn("Cost").AsString().Unique().WithDefaultValue(0).NotNullable()
                .WithColumn("Enabled").AsBoolean().NotNullable();

            Insert.IntoTable("Settings").Row(new { Name = "MenuColor", Command = "menu", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameBidding", Command = "name", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "LimitNames", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameCloud", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameBarret", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameTifa", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameAeris", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameCaitSith", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameCid", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameRed", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameVincent", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "NameYuffie", Enabled = true });
            Insert.IntoTable("Settings").Row(new { Name = "DateBidding", Command = "date", Enabled = true });
        }

        public override void Down()
        {
            Delete.Table("Settings");
        }
    }

    [Migration(2)]
    public class Migration0002InsertSettingsData: Migration
    {
        public override void Up()
        {
        }

        public override void Down()
        {
            Delete.FromTable("Settings").AllRows();
        }
    }
}