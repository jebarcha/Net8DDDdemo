using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehiculos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vin = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    address_country = table.Column<string>(type: "text", nullable: true),
                    address_department = table.Column<string>(type: "text", nullable: true),
                    address_state = table.Column<string>(type: "text", nullable: true),
                    address_city = table.Column<string>(type: "text", nullable: true),
                    address_street = table.Column<string>(type: "text", nullable: true),
                    price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    price_currency_type = table.Column<string>(type: "text", nullable: true),
                    manteinance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    manteinance_currency_type = table.Column<string>(type: "text", nullable: true),
                    date_last_rent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    accesories = table.Column<int[]>(type: "integer[]", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehiculos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    price_by_period_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    price_by_period_currency_type = table.Column<string>(type: "text", nullable: true),
                    manteinance_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    manteinance_currency_type = table.Column<string>(type: "text", nullable: true),
                    accesories_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    accesories_currency_type = table.Column<string>(type: "text", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    total_currency_type = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    duration_start = table.Column<DateOnly>(type: "date", nullable: false),
                    duration_end = table.Column<DateOnly>(type: "date", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejected_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    completed_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    cancellation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rentals", x => x.id);
                    table.ForeignKey(
                        name: "fk_rentals_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rentals_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rent_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_rentals_rent_id",
                        column: x => x.rent_id,
                        principalTable: "rentals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_reviews_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_rentals_user_id",
                table: "rentals",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_rentals_vehiculo_id",
                table: "rentals",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_rent_id",
                table: "reviews",
                column: "rent_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_vehiculo_id",
                table: "reviews",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "vehiculos");
        }
    }
}
