using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb1EntityFramework.Migrations
{
    public partial class ChangedLeaveTypePropsToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LeaveType",
                table: "LeaveApplies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LeaveType",
                table: "LeaveApplies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
