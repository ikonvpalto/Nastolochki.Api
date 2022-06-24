using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Database.Migrations
{
    public partial class MakeEventTimeMoreComplex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TimeIsVoted",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TimeIsVoting",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeIsVoted",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TimeIsVoting",
                table: "Events");
        }
    }
}
