using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeYou.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Month = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Day = table.Column<byte>(type: "tinyint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusOrder",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2025, 5, 6, 21, 11, 26, 987, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, defaultValue: ""),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BaseDuration = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Symbol = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2025, 5, 6, 21, 11, 27, 299, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, defaultValue: ""),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canton",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProvinceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canton", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canton_Province",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TypeServiceId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_TypeService",
                        column: x => x.TypeServiceId,
                        principalTable: "TypeService",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnitMeasureId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Product_UnitMeasure",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CantonId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_Canton",
                        column: x => x.CantonId,
                        principalTable: "Canton",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Password = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    GenderId = table.Column<long>(type: "bigint", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Gender",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FiscalNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SocialReason = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DistrictId = table.Column<long>(type: "bigint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_District",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchHoliday",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchHoliday_Holiday",
                        column: x => x.HolidayId,
                        principalTable: "Holiday",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BranchHoliday_Sucursal",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchSchedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchSchedule_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BranchSchedule_Schedule",
                        column: x => x.ScheduleId,
                        principalTable: "Schedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeInventory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Hour = table.Column<TimeOnly>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValue: "P"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TokenMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, defaultValue: ""),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenMaster_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBranch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    BranchId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBranch_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBranch_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Telephone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VendorId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Vendor",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BranchScheduleBlock",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    StartHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndHour = table.Column<TimeOnly>(type: "time", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchScheduleBlock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchScheduleBlock_BranchSchedule",
                        column: x => x.BranchScheduleId,
                        principalTable: "BranchSchedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryProduct",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Assignable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mininum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Maximum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdProducto = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Inventory",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryProduct_Product",
                        column: x => x.IdProducto,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    StatusOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_PaymentType",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Reservation",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_StatusOrder",
                        column: x => x.StatusOrderId,
                        principalTable: "StatusOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Tax",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservationDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationDetail_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationDetail_Reservation",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReservationDetail_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservationQuestion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<long>(type: "bigint", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationQuestion_Reservation",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InventoryProductTransaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryProductId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryProductTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryProductTransaction_InventoryProduct",
                        column: x => x.InventoryProductId,
                        principalTable: "InventoryProduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PaymentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    TaxId = table.Column<long>(type: "bigint", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Order",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_PaymentType",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Tax",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    LineNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: true),
                    LineNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    Tax = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Invoice",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_Service",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailProduct",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailProduct_OrderDetail",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetailProduct_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetailProduct",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDetailId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetailProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceDetailProduct_InvoiceDetail",
                        column: x => x.InvoiceDetailId,
                        principalTable: "InvoiceDetail",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InvoiceDetailProduct_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branch_DistrictId",
                table: "Branch",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHoliday_BranchId",
                table: "BranchHoliday",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchHoliday_HolidayId",
                table: "BranchHoliday",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSchedule_BranchId",
                table: "BranchSchedule",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchSchedule_ScheduleId",
                table: "BranchSchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchScheduleBlock_BranchSchedule",
                table: "BranchScheduleBlock",
                column: "BranchScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Canton_ProvinceId",
                table: "Canton",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_VendorId",
                table: "Contact",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DistrictId",
                table: "Customer",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CantonId",
                table: "District",
                column: "CantonId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_BranchId",
                table: "Inventory",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_IdProducto",
                table: "InventoryProduct",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_InventoryId",
                table: "InventoryProduct",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProduct_ProductId",
                table: "InventoryProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryProductTransaction_InventoryProductId",
                table: "InventoryProductTransaction",
                column: "InventoryProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BranchId",
                table: "Invoice",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderId",
                table: "Invoice",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PaymentTypeId",
                table: "Invoice",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_TaxId",
                table: "Invoice",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceId",
                table: "InvoiceDetail",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_ProductId",
                table: "InvoiceDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_ServiceId",
                table: "InvoiceDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetailProduct_InvoiceDetailId",
                table: "InvoiceDetailProduct",
                column: "InvoiceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetailProduct_ProductId",
                table: "InvoiceDetailProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BranchId",
                table: "Order",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentTypeId",
                table: "Order",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ReservationId",
                table: "Order",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusOrderId",
                table: "Order",
                column: "StatusOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_TaxId",
                table: "Order",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductId",
                table: "OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ServiceId",
                table: "OrderDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailProduct_OrderDetailId",
                table: "OrderDetailProduct",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailProduct_ProductId",
                table: "OrderDetailProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_UnitMeasureId",
                table: "Product",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_BranchId",
                table: "Reservation",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetail_ProductId",
                table: "ReservationDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetail_ReservationId",
                table: "ReservationDetail",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetail_ServiceId",
                table: "ReservationDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationQuestion_ReservationId",
                table: "ReservationQuestion",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TypeServiceId",
                table: "Service",
                column: "TypeServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TokenMaster_UserId",
                table: "TokenMaster",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DistrictId",
                table: "User",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderId",
                table: "User",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_BranchId",
                table: "UserBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBranch_UserId",
                table: "UserBranch",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_DistrictId",
                table: "Vendor",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchHoliday");

            migrationBuilder.DropTable(
                name: "BranchScheduleBlock");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "InventoryProductTransaction");

            migrationBuilder.DropTable(
                name: "InvoiceDetailProduct");

            migrationBuilder.DropTable(
                name: "OrderDetailProduct");

            migrationBuilder.DropTable(
                name: "ReservationDetail");

            migrationBuilder.DropTable(
                name: "ReservationQuestion");

            migrationBuilder.DropTable(
                name: "TokenMaster");

            migrationBuilder.DropTable(
                name: "UserBranch");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "BranchSchedule");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "InventoryProduct");

            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "UnitMeasure");

            migrationBuilder.DropTable(
                name: "TypeService");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "StatusOrder");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Canton");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
