using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraintForPhoneNumberAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfcde552-8dc3-4ebe-b19b-e13470264888", "AQAAAAIAAYagAAAAEKWQP5+AYIClgAPypUu36eLfWbNn3LtNPc/7kmXATTC8AdWnH0BKaU2IpX2b3q5hLg==", "a5bdd334-4c9e-47dc-a805-803cb542126c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27000b36-d338-400d-8f90-d6fcd29df5c5", "AQAAAAIAAYagAAAAEHbfm8fOwOcXIjKmvY78TL8q+5kkc6VLpKm/3daMpihIa95XlGMNT/Xkc7JuTkMjzA==", "085f03d5-3fa7-4fd3-b2d4-ccd6c3cfb8d1" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cf3a8f1-733a-45da-9581-1bd9660b942d", "AQAAAAIAAYagAAAAEA8VZd7+cJLVAF8iwnk1q7E+/q46qdT60XfkJ0QY0xVdude7SzUGFGnhBPp18nDK4A==", "907d9d6e-e2fd-4bc4-9976-c5395a8d4951" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48b1f670-ae97-4acf-b9ca-89df8d8c853d", "AQAAAAIAAYagAAAAEBm3cfJhKdtYjIZtKXB2LFeIWwwNLdF6zfAqxp+8B+O7OScKKDlTktt45F+XS3CSyw==", "b09f6218-a1d7-44c1-8643-954680ed0c35" });
        }
    }
}
