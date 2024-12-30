﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneToUserDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "AspNetUsers");
        }
    }
}
