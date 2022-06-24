using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvpbldsck.NastolochkiAPI.Events.Api.Database.Migrations
{
    public partial class AddDebugData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
#if DEBUG

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new []{ "Guid", "Address", "Name" },
                values: new object[,]
                {
                    { "660fcee5-0a8e-4032-abef-e58bb7db0a43", "ул. Гикало 129", "У Иры" },
                    { "4bb8a201-80cb-425e-aae9-c2ee2294a9f4", "пр. Пушкинский 47", "У Валеры" },
                    { "fdff3fff-be4d-4575-adaf-33b5a36e2b5c", "пл. Настолок 518", "ТТБХ" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Guid", "Name", "Description", "LocationGuid", "TimeIsVoted", "TimeIsVoting" },
                values: new object[,]
                {
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "Настолки у Валеры", "", "4bb8a201-80cb-425e-aae9-c2ee2294a9f4", true,  false },
                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "Настолки у Иры",    "", "660fcee5-0a8e-4032-abef-e58bb7db0a43", true,  false },
                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "Настолки в ТТБХ",   "", "fdff3fff-be4d-4575-adaf-33b5a36e2b5c", true,  false },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", "Настолки у Валеры", "", "4bb8a201-80cb-425e-aae9-c2ee2294a9f4", false, true },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", "Настолки у ТТБХ",   "", "fdff3fff-be4d-4575-adaf-33b5a36e2b5c", false, true },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", "Настолки у ТТБХ",   "", "fdff3fff-be4d-4575-adaf-33b5a36e2b5c", false, true }
                });

            migrationBuilder.InsertData(
                table: "EventTimeVariants",
                columns: new[] { "EventGuid", "Time" },
                values: new object[,]
                {
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", DateTime.Today.AddHours(18).AddMinutes(30) },

                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", DateTime.Today.AddHours(19) },

                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", DateTime.Today.AddDays(1).AddHours(20) },

                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", DateTime.Today.AddDays(1).AddHours(19) },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", DateTime.Today.AddDays(2).AddHours(19) },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", DateTime.Today.AddDays(3).AddHours(18).AddMinutes(30) },

                    { "98a83780-b240-459f-8a84-5d21b588a48d", DateTime.Today.AddDays(1).AddHours(19) },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", DateTime.Today.AddDays(2).AddHours(19) },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", DateTime.Today.AddDays(3).AddHours(18).AddMinutes(30) },

                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", DateTime.Today.AddDays(1).AddHours(19) },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", DateTime.Today.AddDays(2).AddHours(19) },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", DateTime.Today.AddDays(3).AddHours(18).AddMinutes(30) }
                });

            migrationBuilder.InsertData(
                table: "EventParticipants",
                columns: new[] { "EventGuid", "ParticipantGuid" },
                values: new object[,]
                {
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" },

                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" },

                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" },

                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "50ec2522-bf53-423a-b63b-90fdd9d1781b", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" },

                    { "98a83780-b240-459f-8a84-5d21b588a48d", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "98a83780-b240-459f-8a84-5d21b588a48d", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" },

                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", "2e4888a2-750e-4b54-b944-1e36c3f9f665" },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", "b7674dc1-a85b-4034-a2c4-6a5d21b5eadc" },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", "49d55c7c-def7-4159-bd5f-e519229038e3" },
                    { "5621b08c-7730-41a6-b0a6-fa8def86ecc1", "67e6d70a-c0da-487b-bf59-3c43a5a125a3" }
                });

#endif
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
#if DEBUG

            migrationBuilder.Sql("DELETE * FROM \"EventParticipants\" WHERE \"EventGuid\" in ('ae9d4661-29c0-41f3-8229-8332c9bb8d07', 'bd30d3f9-b46f-4b96-b696-d1dcf3a344fa', '50ec2522-bf53-423a-b63b-90fdd9d1781b', 'e869f8eb-4d09-41c7-91c1-3aab4ba7c156', '98a83780-b240-459f-8a84-5d21b588a48d', '5621b08c-7730-41a6-b0a6-fa8def86ecc1')");

            migrationBuilder.Sql("DELETE * FROM \"EventTimeVariants\" WHERE \"EventGuid\" in ('ae9d4661-29c0-41f3-8229-8332c9bb8d07', 'bd30d3f9-b46f-4b96-b696-d1dcf3a344fa', '50ec2522-bf53-423a-b63b-90fdd9d1781b', 'e869f8eb-4d09-41c7-91c1-3aab4ba7c156', '98a83780-b240-459f-8a84-5d21b588a48d', '5621b08c-7730-41a6-b0a6-fa8def86ecc1')");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Guid",
                keyValues: new[] { "ae9d4661-29c0-41f3-8229-8332c9bb8d07", "bd30d3f9-b46f-4b96-b696-d1dcf3a344fa", "50ec2522-bf53-423a-b63b-90fdd9d1781b", "e869f8eb-4d09-41c7-91c1-3aab4ba7c156", "98a83780-b240-459f-8a84-5d21b588a48d", "5621b08c-7730-41a6-b0a6-fa8def86ecc1" });

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Guid",
                keyValues: new[] { "660fcee5-0a8e-4032-abef-e58bb7db0a43", "4bb8a201-80cb-425e-aae9-c2ee2294a9f4", "fdff3fff-be4d-4575-adaf-33b5a36e2b5c" });

#endif
        }
    }
}
