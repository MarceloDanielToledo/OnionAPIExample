using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnInPayment_AddSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RefundStatuses");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "RefundStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PaymentStatuses");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PaymentStatuses");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Payments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PaymentStatuses",
                columns: new[] { "Id", "Details", "Status" },
                values: new object[,]
                {
                    { 1, "Payment pending confirmation.", "REQUEST" },
                    { 2, "Cancelled payment.", "CANCELLED" },
                    { 3, "Payment confirmed.", "CONFIRMED" },
                    { 4, "Payment undone.", "UNDONE" }
                });

            migrationBuilder.InsertData(
                table: "RefundStatuses",
                columns: new[] { "Id", "Details", "Status" },
                values: new object[,]
                {
                    { 1, "Refund pending confirmation.", "REQUEST" },
                    { 2, "Cancelled Refund.", "CANCELLED" },
                    { 3, "Refund confirmed.", "CONFIRMED" },
                    { 4, "Refund undone.", "UNDONE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PaymentStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RefundStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RefundStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RefundStatuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RefundStatuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RefundStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "RefundStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PaymentStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PaymentStatuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
