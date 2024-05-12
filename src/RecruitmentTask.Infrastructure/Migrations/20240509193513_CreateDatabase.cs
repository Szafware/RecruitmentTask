using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Address_ApartmentNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    Address_HouseNumber = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    Address_StreetName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Address_Town = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PersonalData_BirthDateUtc = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    PersonalData_FirstName = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    PersonalData_LastName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PersonalData_PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
