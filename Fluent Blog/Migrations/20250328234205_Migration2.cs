using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "SMALLDATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldDefaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Post",
                type: "SMALLDATETIME",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "SMALLDATETIME",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
