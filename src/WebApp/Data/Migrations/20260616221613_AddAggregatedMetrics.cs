using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeuValorLiquido.WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAggregatedMetrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aggregated_metrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MetricDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EventType = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Dimension = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aggregated_metrics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aggregated_metrics_MetricDate_EventType_Dimension",
                table: "aggregated_metrics",
                columns: new[] { "MetricDate", "EventType", "Dimension" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aggregated_metrics");
        }
    }
}
