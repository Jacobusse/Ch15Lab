using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ch15Lab.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SalesTax = table.Column<decimal>(type: "NUMERIC", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    StateId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_Vendor_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    BillingAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShippingAddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    SalesTax = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Address_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    InventoryItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SkuNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    VendorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cost = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    RetailPrice = table.Column<decimal>(type: "NUMERIC", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItem_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "VendorId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetail",
                columns: table => new
                {
                    PurchaseOrderDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PurchaseOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    InventroyItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetail", x => x.PurchaseOrderDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_InventoryItem_InventroyItemId",
                        column: x => x.InventroyItemId,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemId");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusId");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetail_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "PurchaseOrderId");
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "Description" },
                values: new object[,]
                {
                    { 1, "Ordered" },
                    { 2, "Shipped" },
                    { 3, "Delivered" },
                    { 4, "Paid" }
                });

            migrationBuilder.InsertData(
                table: "State",
                columns: new[] { "StateId", "Code", "Name", "SalesTax" },
                values: new object[,]
                {
                    { 17, "IL", "Illinois", 0.0882m },
                    { 18, "IN", "Indiana", 0.07m },
                    { 26, "MI", "Michigan", 0.06m },
                    { 39, "OH", "Ohio", 0.0723m },
                    { 55, "WI", "Wisconsin", 0.0543m }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressId", "City", "PostalCode", "StateId", "Street", "Title" },
                values: new object[,]
                {
                    { 10001, "Holland", "49424", 26, "321 Fisrt Ave.", "Ox Exports Main Office" },
                    { 10002, "Holland", "49424", 26, "123 Last Ln.", "Ox Exports Warehouse" },
                    { 10003, "Columbus", "43229", 39, "987 Easy St.", "Nicks Nacks Headquarters" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "AddressId", "Name" },
                values: new object[,]
                {
                    { 1001, 10001, "Ox Imports" },
                    { 1002, 10003, "Nicks Nacks" }
                });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "VendorId", "AddressId", "Name" },
                values: new object[] { 101, 10001, "Ox Exports" });

            migrationBuilder.InsertData(
                table: "InventoryItem",
                columns: new[] { "InventoryItemId", "Cost", "Count", "Description", "RetailPrice", "SkuNumber", "Title", "VendorId" },
                values: new object[,]
                {
                    { 20001, 85.17m, 17, "This particular vase features a traditional windmill design combined with the floral motif, handpainted in blue & white. This piece is approximately 30cm (12\") tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity.", 141.95m, "010095_11", "DeWit Handpainted 12\" Blue Delft Vase", 101 },
                    { 20002, 35.97m, 8, "This particular vase features either a traditional windmill or floral design handpainted in blue & white. This piece is approximately 8\" tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity.", 59.95m, "010096_1x", "DeWit Handpainted 8\" Blue Delft Vase", 101 },
                    { 20003, 51.57m, 6, "This particular vase features a traditional floral design, handpainted in blue & white. This piece is approximately 22cm (9\") tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity.", 85.95m, "011947_12", "DeWit Handpainted Blue & White Delft Six-Sided Vase", 101 },
                    { 20004, 32.37m, 72, "Available in either the classic floral design or the traditional windmill design, this charming hand-painted ornament is a great combination of two favorites: real, true hand-painted, made-in-Holland delft, and the collectability of Christmas items. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This ornament is approximately 7cm (3\") tall.", 53.95m, "011527_xx", "DeWit Ball Christmas Ornament Floral or Windmill Design", 101 },
                    { 20005, 16.98m, 94, "Featuring either the classic floral or windmill design, these charming hand-painted ornaments are a great combination of two favorites: real, true hand-painted, made-in-Holland delft, and the collectibility of Christmas items. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This ornament is approximately 6cm (2.5\") tall.", 33.95m, "011524_xx", "DeWit Top Shape Christmas Ornament", 101 },
                    { 20006, 64.17m, 3, "This wonderful hand-painted delft teapot will make tea-time a truly special event! This teapot is available with either a traditional windmill or floral design. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This item is approximately 6.25\" tall, by 10\" wide.", 106.95m, "010761_1x", "DeWit Delft Teapot, 6.25\"", 101 },
                    { 20007, 71.97m, 2, "This wonderful hand-painted delft teapot will make tea-time a truly special event! This teapot is available with either a traditional windmill or floral design. Select your design choice above. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This item is approximately 10\" tall.", 119.95m, "010760_1x", "DeWit Delft Handpainted Teapot", 101 },
                    { 20008, 80.97m, 12, "This beautiful hand-painted set will make tea-time a truly special event! The sugar & creamer set is available with either a traditional windmill or floral design, handpainted in blue & white. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity.", 134.95m, "010762_1x", "DeWit Handpainted Blue Delft Sugar & Creamer", 101 },
                    { 20009, 98.37m, 9, "This charming Tea-For-One set is handpainted in Holland and is available in the traditional blue and white windmill motif, the beautiful blue and white floral design, or the stunning multi-color floral painting. Teapot holds approximately 14 oz of liquid. A beautiful gift for yourself or for the tea lover in your life. Please select your design choice from the pull-down menu above. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity.", 163.95m, "011420_xx", "DeWit Handpainted Tea for One Set", 101 },
                    { 20010, 25.17m, 38, "Decorative circular plate is a must-have for the any delft collector. Decorated with a classic Dutch windmill design, this charming plate is the perfect addition to your curio cabinet. Each piece carries the full factory markings, which includes the factory mark, item number, artist's initials, and the date code, and is delivered with a certificate of authenticity. This piece is approximately 5.5\" in diameter.", 41.95m, "010923_1x", "DeWit Handpainted Blue Delft Serving Plate", 101 },
                    { 20011, 20.97m, 45, "Coffee with class and distinction. This mug will quickly become your favorite, both in function and beauty. It is marked with the DeWit factory mark, and comes with a certificate of authenticity! Handpainted with a blue windmill or floral design, this is a true classic. This mug is approximately 4\" tall.", 34.95m, "010777_1x", "DeWit Handpainted Coffee Mug", 101 },
                    { 20012, 37.17m, 16, "This particular windmill features a traditional blue and white windmill design handpainted on the windmill itself. This piece is approximately 9.5\" tall. Each DeWit piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity.", 61.95m, "010483_12", "DeWit Handpainted Blue and White Delft Ceramic Windmill - 9.5\"", 101 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrder",
                columns: new[] { "PurchaseOrderId", "BillingAddressId", "CustomerId", "Date", "Note", "Number", "SalesTax", "ShippingAddressId" },
                values: new object[] { 5001, 10003, 1001, new DateOnly(2023, 10, 22), "November Resupply", "NN6003", 0.0543m, 10003 });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetail",
                columns: new[] { "PurchaseOrderDetailId", "InventroyItemId", "OrderStatusId", "PurchaseOrderId", "Quantity", "UnitPrice" },
                values: new object[] { 50001, 20001, 1, 5001, 3, 141.95m });

            migrationBuilder.CreateIndex(
                name: "IX_Address_StateId",
                table: "Address",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_AddressId",
                table: "Customer",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_VendorId",
                table: "InventoryItem",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_BillingAddressId",
                table: "PurchaseOrder",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CustomerId",
                table: "PurchaseOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_ShippingAddressId",
                table: "PurchaseOrder",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_InventroyItemId",
                table: "PurchaseOrderDetail",
                column: "InventroyItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_OrderStatusId",
                table: "PurchaseOrderDetail",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetail_PurchaseOrderId",
                table: "PurchaseOrderDetail",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_AddressId",
                table: "Vendor",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderDetail");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
