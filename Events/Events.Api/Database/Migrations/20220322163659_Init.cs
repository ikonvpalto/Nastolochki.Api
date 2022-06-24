using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Database.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Name = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false),
                    LocationGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationGuid",
                        column: x => x.LocationGuid,
                        principalTable: "Locations",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.SetDefault);
                });

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    EventGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipant", x => new { x.EventGuid, x.ParticipantGuid });
                    table.ForeignKey(
                        name: "FK_EventParticipants_Events_EventGuid",
                        column: x => x.EventGuid,
                        principalTable: "Events",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTimeVariants",
                columns: table => new
                {
                    EventGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTimeVariant", x => new { x.EventGuid, x.Time });
                    table.ForeignKey(
                        name: "FK_EventTimeVariants_Events_EventGuid",
                        column: x => x.EventGuid,
                        principalTable: "Events",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationGuid",
                table: "Events",
                column: "LocationGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropTable(
                name: "EventTimeVariants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
