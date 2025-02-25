using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimoshStore.Migrations
{
    /// <inheritdoc />
    public partial class FixRelBlogTagsSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    IconCssClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    imageUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SeenAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountRate = table.Column<byte>(type: "smallint", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    HasSellerRequest = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEntity_RoleEntity_RoleId",
                        column: x => x.RoleId,
                        principalTable: "RoleEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderCode = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SellerId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    DiscountId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    StockAmount = table.Column<byte>(type: "smallint", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductDiscounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "ProductDiscounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_UserEntity_SellerId",
                        column: x => x.SellerId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelBlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    BlogEntityId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelBlogCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelBlogCategories_BlogCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelBlogCategories_Blogs_BlogEntityId",
                        column: x => x.BlogEntityId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelBlogCategories_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelBlogTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false),
                    BlogEntityId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelBlogTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelBlogTags_BlogTags_TagId",
                        column: x => x.TagId,
                        principalTable: "BlogTags",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelBlogTags_Blogs_BlogEntityId",
                        column: x => x.BlogEntityId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelBlogTags_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    StarCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 3),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductComments_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BlogCategories",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "Beauty" },
                    { 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "Food" },
                    { 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "Life Style" },
                    { 4, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "Travel" },
                    { 5, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "Fashion" },
                    { 7, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1030), new TimeSpan(0, 0, 0, 0, 0)), "Education" },
                    { 8, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1030), new TimeSpan(0, 0, 0, 0, 0)), "Entertainment" },
                    { 9, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 793, DateTimeKind.Unspecified).AddTicks(1030), new TimeSpan(0, 0, 0, 0, 0)), "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "CreatedAt", "IconCssClass", "Name", "imageUrl" },
                values: new object[,]
                {
                    { 1, "Blue", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 782, DateTimeKind.Unspecified).AddTicks(1190), new TimeSpan(0, 0, 0, 0, 0)), "", "Fresh Meat", "/images/category/category-1.jpg" },
                    { 2, "Red", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 782, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), "", "Vegetables", "/images/category/category-2.jpg" },
                    { 3, "Green", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 782, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), "", "Fresh Fruits", "/images/category/category-3.jpg" },
                    { 4, "Brown", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 782, DateTimeKind.Unspecified).AddTicks(1310), new TimeSpan(0, 0, 0, 0, 0)), "", "Dried Fruits & Nuts", "/images/category/category-4.jpg" },
                    { 5, "Purple", new DateTimeOffset(new DateTime(2025, 2, 23, 17, 42, 49, 782, DateTimeKind.Unspecified).AddTicks(1330), new TimeSpan(0, 3, 0, 0, 0)), "", "Ocean Foods", "/images/category/category-5.jpg" },
                    { 6, "Yellow", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 788, DateTimeKind.Unspecified).AddTicks(2190), new TimeSpan(0, 0, 0, 0, 0)), "", "Butter & Eggs", "/images/category/category-6.jpg" },
                    { 7, "Pink", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 788, DateTimeKind.Unspecified).AddTicks(2190), new TimeSpan(0, 0, 0, 0, 0)), "", "Fastfood", "/images/category/category-7.jpg" },
                    { 8, "Grey", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 788, DateTimeKind.Unspecified).AddTicks(2190), new TimeSpan(0, 0, 0, 0, 0)), "", "Oatmeal", "/images/category/category-8.jpg" },
                    { 9, "Orange", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 788, DateTimeKind.Unspecified).AddTicks(2190), new TimeSpan(0, 0, 0, 0, 0)), "", "Juices", "/images/category/category-9.jpg" }
                });

            migrationBuilder.InsertData(
                table: "ProductDiscounts",
                columns: new[] { "Id", "CreatedAt", "DiscountRate", "Enabled", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2610), new TimeSpan(0, 0, 0, 0, 0)), (byte)10, true, new DateTimeOffset(new DateTime(2025, 8, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2390), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2260), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), (byte)20, true, new DateTimeOffset(new DateTime(2025, 8, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), (byte)30, true, new DateTimeOffset(new DateTime(2025, 8, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 791, DateTimeKind.Unspecified).AddTicks(2620), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RoleEntity",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, 0, 0, 0, 0)), "admin" },
                    { 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, 0, 0, 0, 0)), "seller" },
                    { 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(7700), new TimeSpan(0, 0, 0, 0, 0)), "buyer" }
                });

            migrationBuilder.InsertData(
                table: "UserEntity",
                columns: new[] { "Id", "CreatedAt", "Email", "Enabled", "FirstName", "HasSellerRequest", "LastName", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(890), new TimeSpan(0, 0, 0, 0, 0)), "admin@siliconmade.com", true, "admin", false, "admin", "1234", 1 },
                    { 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "seller@siliconmade.com", true, "seller", false, "seller", "1234", 2 },
                    { 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 777, DateTimeKind.Unspecified).AddTicks(1020), new TimeSpan(0, 0, 0, 0, 0)), "buyer@siliconmade.com", true, "buyer", false, "buyer", "1234", 3 }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Content", "CreatedAt", "Enabled", "ImageUrl", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Visiting a clean farm in the US is an enlightening experience. The farm, with its well-maintained fields and healthy livestock, showcases the best of American agriculture. The cleanliness of the farm is a testament to the hard work and dedication of the farmers. They follow strict hygiene and cleanliness standards to ensure the health and safety of their animals and crops. The farm also uses sustainable farming practices to protect the environment. A visit to such a farm is not only educational but also inspiring, as it shows the importance of cleanliness and sustainability in farming.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1290), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-3.jpg", "Visit the clean farm in the US", 1 },
                    { 2, "Preparing breakfast for a large group requires careful planning and organization. Start by choosing recipes that can be made in large quantities, such as scrambled eggs, pancakes, or a breakfast casserole. Consider making items that can be prepared ahead of time, like muffins or fruit salad. Set up a serving station with all the necessary utensils, plates, and cups. Cook as much as you can in advance to minimize stress on the day of the event. Remember to cater to different dietary needs and preferences. With these tips, you'll be able to host a successful breakfast for 30 people.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-2.jpg", "6 ways to prepare breakfast for 30", 1 },
                    { 3, "Cooking can be a daunting task for many, especially when it comes to preparing meals for a family or a large group. However, with the right tips and techniques, it can be made simple and enjoyable. Start by planning your meals in advance and doing a weekly grocery shop. This not only saves time but also ensures you have all the ingredients you need. Use fresh ingredients wherever possible as they provide the best flavor. Don't be afraid to experiment with different herbs and spices to add a unique twist to your dishes. Remember, the key to simple cooking is preparation and using the right tools. Invest in a good set of knives, pots, and pans. Lastly, don't forget to clean as you go. This will keep your kitchen tidy and make the cooking process much more manageable.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-1.jpg", "Cooking tips make cooking simple", 1 },
                    { 4, "Organic food has become increasingly popular in recent years, with many people choosing to buy organic produce over conventionally grown food. There are several benefits to eating organic food, including better taste, higher nutritional value, and fewer pesticides. Organic farming practices are also better for the environment, as they reduce pollution and conserve water and soil. Additionally, organic food is often fresher and free from harmful additives and preservatives. While organic food can be more expensive, many people believe the health benefits are worth the extra cost. Overall, eating organic food is a great way to support your health and the environment.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-4.jpg", "The benefits of eating organic food", 1 },
                    { 5, "Fruits are an essential part of a healthy diet, providing essential vitamins, minerals, and fiber. However, it's essential to consume them in moderation and avoid overeating. While fruits are nutritious, they also contain natural sugars that can contribute to weight gain if consumed in excess. To include fruits in your diet, aim to eat a variety of colors and types to ensure you're getting a wide range of nutrients. Fresh, frozen, and dried fruits are all healthy options, but be mindful of added sugars in canned or packaged fruits. Avoid fruit juices and smoothies, as they can be high in sugar and calories. Instead, opt for whole fruits, which provide more fiber and nutrients. By following these tips, you can enjoy the health benefits of fruits without overdoing it.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1300), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-5.jpg", "How to (and not to) include fruits in your diet", 1 },
                    { 6, "Garlic is a popular ingredient in many dishes, known for its strong flavor and health benefits. However, there are times when you may need to remove garlic from the menu. Some people are allergic to garlic and can experience symptoms like hives, itching, or difficulty breathing after consuming it. If you're cooking for someone with a garlic allergy, it's essential to avoid using garlic in your dishes. Additionally, some people may have a sensitivity to garlic that causes digestive issues like bloating or gas. In these cases, it's best to limit or avoid garlic in your cooking. While garlic is a versatile and flavorful ingredient, it's essential to be mindful of people's dietary needs and preferences when preparing meals.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1310), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-6.jpg", "The moment You Need To Remove Garlic From The Menu", 1 },
                    { 7, "Organic food has become increasingly popular in recent years, with many people choosing to buy organic produce over conventionally grown food. While organic food is often more expensive than non-organic options, there are several factors that contribute to the higher cost. Organic farming practices are more labor-intensive and require more time and effort than conventional farming methods. Additionally, organic farmers often pay higher prices for organic seeds, fertilizers, and pest control methods. The certification process for organic food is also costly, as farmers must meet strict standards to be certified organic. While the cost of organic food can be a barrier for some consumers, many people believe the health and environmental benefits are worth the extra expense.", new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 792, DateTimeKind.Unspecified).AddTicks(1310), new TimeSpan(0, 0, 0, 0, 0)), true, "/theme/img/blog/blog-1.jpg", "Cost anaylsis of organic food", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountId", "Enabled", "Name", "Price", "SellerId", "StockAmount" },
                values: new object[,]
                {
                    { 1, 9, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7840), new TimeSpan(0, 0, 0, 0, 0)), "Mixed Fruit Juice", 2, true, "Les Benjamins", 199m, 2, (byte)100 },
                    { 2, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7950), new TimeSpan(0, 0, 0, 0, 0)), "Mango", 2, true, "Mango", 56m, 2, (byte)50 },
                    { 3, 7, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7950), new TimeSpan(0, 0, 0, 0, 0)), "Hamburger", 2, true, "Hamburger", 537m, 2, (byte)20 },
                    { 4, 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7960), new TimeSpan(0, 0, 0, 0, 0)), "Meat", null, true, "Red Meat", 455m, 2, (byte)50 },
                    { 5, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7960), new TimeSpan(0, 0, 0, 0, 0)), "Banana", null, true, "Banana", 431m, 2, (byte)75 },
                    { 6, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7960), new TimeSpan(0, 0, 0, 0, 0)), "Fig", null, true, "Fig", 224m, 2, (byte)100 },
                    { 7, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Apple", null, true, "Apple", 76m, 2, (byte)80 },
                    { 8, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Grapes", null, true, "Grapes", 430m, 2, (byte)100 },
                    { 9, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Watermelon", null, true, "Watermelon", 249m, 2, (byte)20 },
                    { 10, 4, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Raisins", null, true, "Raisins", 500m, 2, (byte)100 },
                    { 11, 9, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Orange Juice", null, true, "Orange Juice", 488m, 2, (byte)100 },
                    { 12, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Mixed Fruits", null, true, "Mixed Fruits", 65m, 2, (byte)100 },
                    { 13, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Spinach", null, true, "Spinach", 104m, 2, (byte)100 },
                    { 14, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, 0, 0, 0, 0)), "Bell Pepper", null, true, "Bell Pepper", 522m, 2, (byte)100 },
                    { 15, 7, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 779, DateTimeKind.Unspecified).AddTicks(7980), new TimeSpan(0, 0, 0, 0, 0)), "Fried Chicken", null, true, "Fried Chicken", 193m, 2, (byte)20 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Url" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7090), new TimeSpan(0, 0, 0, 0, 0)), 1, "/images/product/product-1.jpg" },
                    { 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 0, 0, 0, 0)), 2, "/images/product/product-2.png" },
                    { 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 0, 0, 0, 0)), 3, "/images/product/product-3.jpg" },
                    { 4, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 0, 0, 0, 0)), 4, "/images/product/product-4.png" },
                    { 5, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 0, 0, 0, 0)), 5, "/images/product/product-5.png" },
                    { 6, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 6, "/images/product/product-6.jpg" },
                    { 7, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 7, "/images/product/product-7.jpg" },
                    { 8, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 8, "/images/product/product-8.jpg" },
                    { 9, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 9, "/images/product/product-9.jpg" },
                    { 10, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 10, "/images/product/product-10.jpg" },
                    { 11, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 11, "/images/product/product-11.jpg" },
                    { 12, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 12, "/images/product/product-12.jpg" },
                    { 13, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 13, "/images/product/product-13.jpg" },
                    { 14, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 14, "/images/product/product-14.jpg" },
                    { 15, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 780, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 0, 0, 0, 0)), 15, "/images/product/product-15.jpg" }
                });

            migrationBuilder.InsertData(
                table: "RelBlogCategories",
                columns: new[] { "Id", "BlogEntityId", "BlogId", "CategoryId", "CreatedAt" },
                values: new object[,]
                {
                    { 1, null, 1, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9760), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2, null, 1, 4, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3, null, 2, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 4, null, 2, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 5, null, 3, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 6, null, 4, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 7, null, 5, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9770), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 8, null, 6, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9780), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 9, null, 7, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 797, DateTimeKind.Unspecified).AddTicks(9780), new TimeSpan(0, 0, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "RelBlogTags",
                columns: new[] { "Id", "BlogEntityId", "BlogId", "CreatedAt", "TagId" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(220), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, null, 1, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 3, null, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 4, null, 2, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 5, null, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), 5 },
                    { 6, null, 3, new DateTimeOffset(new DateTime(2025, 2, 23, 14, 42, 49, 796, DateTimeKind.Unspecified).AddTicks(230), new TimeSpan(0, 0, 0, 0, 0)), 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_ProductId",
                table: "ProductComments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountId",
                table: "Products",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogCategories_BlogEntityId",
                table: "RelBlogCategories",
                column: "BlogEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogCategories_BlogId_CategoryId",
                table: "RelBlogCategories",
                columns: new[] { "BlogId", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogCategories_CategoryId",
                table: "RelBlogCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogTags_BlogEntityId",
                table: "RelBlogTags",
                column: "BlogEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogTags_BlogId",
                table: "RelBlogTags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_RelBlogTags_TagId",
                table: "RelBlogTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_Email",
                table: "UserEntity",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEntity_RoleId",
                table: "UserEntity",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ContactForms");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "ProductComments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "RelBlogCategories");

            migrationBuilder.DropTable(
                name: "RelBlogTags");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "BlogTags");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ProductDiscounts");

            migrationBuilder.DropTable(
                name: "UserEntity");

            migrationBuilder.DropTable(
                name: "RoleEntity");
        }
    }
}
