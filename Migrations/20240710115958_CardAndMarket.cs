using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class CardAndMarket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Supertype = table.Column<string>(type: "text", nullable: false),
                    Subtypes = table.Column<string>(type: "text", nullable: true),
                    Hp = table.Column<string>(type: "text", nullable: true),
                    Types = table.Column<string>(type: "text", nullable: true),
                    Abilities = table.Column<string>(type: "text", nullable: true),
                    Attacks = table.Column<string>(type: "text", nullable: true),
                    Weaknesses = table.Column<string>(type: "text", nullable: true),
                    RetreatCost = table.Column<string>(type: "text", nullable: true),
                    ConvertedRetreatCost = table.Column<int>(type: "integer", nullable: false),
                    SetId = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    Rarity = table.Column<string>(type: "text", nullable: true),
                    SmallImageUrl = table.Column<string>(type: "text", nullable: false),
                    LargeImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardId = table.Column<string>(type: "text", nullable: false),
                    LowPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MidPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    HighPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    MarketPriceValue = table.Column<decimal>(type: "numeric", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "MarketPrices");
        }
    }
}
