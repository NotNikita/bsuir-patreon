using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class SubscriptionType1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionType_SubId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionType_AspNetUsers_AuthorId",
                table: "SubscriptionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionType",
                table: "SubscriptionType");

            migrationBuilder.RenameTable(
                name: "SubscriptionType",
                newName: "SubscriptionTypes");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionType_AuthorId",
                table: "SubscriptionTypes",
                newName: "IX_SubscriptionTypes_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionTypes",
                table: "SubscriptionTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubId",
                table: "Subscriptions",
                column: "SubId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionTypes_AspNetUsers_AuthorId",
                table: "SubscriptionTypes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionTypes_SubId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionTypes_AspNetUsers_AuthorId",
                table: "SubscriptionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubscriptionTypes",
                table: "SubscriptionTypes");

            migrationBuilder.RenameTable(
                name: "SubscriptionTypes",
                newName: "SubscriptionType");

            migrationBuilder.RenameIndex(
                name: "IX_SubscriptionTypes_AuthorId",
                table: "SubscriptionType",
                newName: "IX_SubscriptionType_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubscriptionType",
                table: "SubscriptionType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionType_SubId",
                table: "Subscriptions",
                column: "SubId",
                principalTable: "SubscriptionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionType_AspNetUsers_AuthorId",
                table: "SubscriptionType",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
