using Microsoft.EntityFrameworkCore;
using Ch15Lab.Data.Entities;

namespace Ch15Lab.Data.Contexts;

public partial class POManagerContext : DbContext
{
    public POManagerContext() { }

    public POManagerContext(DbContextOptions<POManagerContext> options) : base(options) { }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<InventoryItem> InventoryItems { get; set; }
    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=.\\POManager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderStatusId)
                .ValueGeneratedNever();

            entity.HasData(
                new OrderStatus() { OrderStatusId = 1, Description = "Ordered" },
                new OrderStatus() { OrderStatusId = 2, Description = "Shipped" },
                new OrderStatus() { OrderStatusId = 3, Description = "Delivered" },
                new OrderStatus() { OrderStatusId = 4, Description = "Paid" }
            );
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.HasOne(d => d.State)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Added type
            entity.HasData(
                new Address() { AddressId = 10001, AddressTypeId = 91, Title = "Ox Exports Main Office", Street = "321 Fisrt Ave.", City = "Holland", StateId = 26, PostalCode = "49424" },
                new Address() { AddressId = 10002, AddressTypeId = 92, Title = "Ox Exports Warehouse", Street = "123 Last Ln.", City = "Holland", StateId = 26, PostalCode = "49424" },
                new Address() { AddressId = 10003, AddressTypeId = 93, Title = "Nicks Nacks Headquarters", Street = "987 Easy St.", City = "Columbus", StateId = 39, PostalCode = "43229" }
            );
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.Customers)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasData(
                new Customer() { CustomerId = 1001, AddressId = 10001, Name = "Ox Imports" },
                new Customer() { CustomerId = 1002, AddressId = 10003, Name = "Nicks Nacks" }
            );
        });

        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.ToTable("InventoryItem");

            entity.Property(e => e.Cost)
                .HasColumnType("NUMERIC");

            entity.Property(e => e.RetailPrice)
                .HasColumnType("NUMERIC");

            entity.HasOne(d => d.Vendor)
                .WithMany(p => p.InventoryItems)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasData(
                new InventoryItem()
                {
                    InventoryItemId = 20001,
                    VendorId = 101,
                    SkuNumber = "010095_11",
                    Cost = 85.17M,
                    RetailPrice = 141.95M,
                    Count = 17,
                    Title = "DeWit Handpainted 12\" Blue Delft Vase",
                    Description = "This particular vase features a traditional windmill design combined with the floral motif, handpainted in blue & white. This piece is approximately 30cm (12\") tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20002,
                    VendorId = 101,
                    SkuNumber = "010096_1x",
                    Cost = 35.97M,
                    RetailPrice = 59.95M,
                    Count = 8,
                    Title = "DeWit Handpainted 8\" Blue Delft Vase",
                    Description = "This particular vase features either a traditional windmill or floral design handpainted in blue & white. This piece is approximately 8\" tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20003,
                    VendorId = 101,
                    SkuNumber = "011947_12",
                    Cost = 51.57M,
                    RetailPrice = 85.95M,
                    Count = 6,
                    Title = "DeWit Handpainted Blue & White Delft Six-Sided Vase",
                    Description = "This particular vase features a traditional floral design, handpainted in blue & white. This piece is approximately 22cm (9\") tall. Each piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20004,
                    VendorId = 101,
                    SkuNumber = "011527_xx",
                    Cost = 32.37M,
                    RetailPrice = 53.95M,
                    Count = 72,
                    Title = "DeWit Ball Christmas Ornament Floral or Windmill Design",
                    Description = "Available in either the classic floral design or the traditional windmill design, this charming hand-painted ornament is a great combination of two favorites: real, true hand-painted, made-in-Holland delft, and the collectability of Christmas items. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This ornament is approximately 7cm (3\") tall."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20005,
                    VendorId = 101,
                    SkuNumber = "011524_xx",
                    Cost = 16.98M,
                    RetailPrice = 33.95M,
                    Count = 94,
                    Title = "DeWit Top Shape Christmas Ornament",
                    Description = "Featuring either the classic floral or windmill design, these charming hand-painted ornaments are a great combination of two favorites: real, true hand-painted, made-in-Holland delft, and the collectibility of Christmas items. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This ornament is approximately 6cm (2.5\") tall."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20006,
                    VendorId = 101,
                    SkuNumber = "010761_1x",
                    Cost = 64.17M,
                    RetailPrice = 106.95M,
                    Count = 3,
                    Title = "DeWit Delft Teapot, 6.25\"",
                    Description = "This wonderful hand-painted delft teapot will make tea-time a truly special event! This teapot is available with either a traditional windmill or floral design. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This item is approximately 6.25\" tall, by 10\" wide."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20007,
                    VendorId = 101,
                    SkuNumber = "010760_1x",
                    Cost = 71.97M,
                    RetailPrice = 119.95M,
                    Count = 2,
                    Title = "DeWit Delft Handpainted Teapot",
                    Description = "This wonderful hand-painted delft teapot will make tea-time a truly special event! This teapot is available with either a traditional windmill or floral design. Select your design choice above. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity. This item is approximately 10\" tall."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20008,
                    VendorId = 101,
                    SkuNumber = "010762_1x",
                    Cost = 80.97M,
                    RetailPrice = 134.95M,
                    Count = 12,
                    Title = "DeWit Handpainted Blue Delft Sugar & Creamer",
                    Description = "This beautiful hand-painted set will make tea-time a truly special event! The sugar & creamer set is available with either a traditional windmill or floral design, handpainted in blue & white. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20009,
                    VendorId = 101,
                    SkuNumber = "011420_xx",
                    Cost = 98.37M,
                    RetailPrice = 163.95M,
                    Count = 9,
                    Title = "DeWit Handpainted Tea for One Set",
                    Description = "This charming Tea-For-One set is handpainted in Holland and is available in the traditional blue and white windmill motif, the beautiful blue and white floral design, or the stunning multi-color floral painting. Teapot holds approximately 14 oz of liquid. A beautiful gift for yourself or for the tea lover in your life. Please select your design choice from the pull-down menu above. Each piece of DeWit delft is marked with the factory's mark, item number, and artist's initials. Also includes a certificate of authenticity."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20010,
                    VendorId = 101,
                    SkuNumber = "010923_1x",
                    Cost = 25.17M,
                    RetailPrice = 41.95M,
                    Count = 38,
                    Title = "DeWit Handpainted Blue Delft Serving Plate",
                    Description = "Decorative circular plate is a must-have for the any delft collector. Decorated with a classic Dutch windmill design, this charming plate is the perfect addition to your curio cabinet. Each piece carries the full factory markings, which includes the factory mark, item number, artist's initials, and the date code, and is delivered with a certificate of authenticity. This piece is approximately 5.5\" in diameter."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20011,
                    VendorId = 101,
                    SkuNumber = "010777_1x",
                    Cost = 20.97M,
                    RetailPrice = 34.95M,
                    Count = 45,
                    Title = "DeWit Handpainted Coffee Mug",
                    Description = "Coffee with class and distinction. This mug will quickly become your favorite, both in function and beauty. It is marked with the DeWit factory mark, and comes with a certificate of authenticity! Handpainted with a blue windmill or floral design, this is a true classic. This mug is approximately 4\" tall."
                },
                new InventoryItem()
                {
                    InventoryItemId = 20012,
                    VendorId = 101,
                    SkuNumber = "010483_12",
                    Cost = 37.17M,
                    RetailPrice = 61.95M,
                    Count = 16,
                    Title = "DeWit Handpainted Blue and White Delft Ceramic Windmill - 9.5\"",
                    Description = "This particular windmill features a traditional blue and white windmill design handpainted on the windmill itself. This piece is approximately 9.5\" tall. Each DeWit piece is completely handpainted, marked with the factory mark, item number, and the artist's initials. Each and every piece comes with a certificate of authenticity."
                }
            );
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.SalesTax)
                .HasColumnType("NUMERIC");

            entity.HasOne(d => d.BillingAddress)
                .WithMany(p => p.PurchaseOrderBillingAddresses)
                .HasForeignKey(d => d.BillingAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShippingAddress)
                .WithMany(p => p.PurchaseOrderShippingAddresses)
                .HasForeignKey(d => d.ShippingAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasData(
                new PurchaseOrder() { PurchaseOrderId = 5001, Number = "NN6003", Date = new DateOnly(2023, 10, 22), CustomerId = 1001, BillingAddressId = 10003, ShippingAddressId = 10003, Note = "November Resupply", SalesTax = 0.0543M }
            );
        });

        modelBuilder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.ToTable("PurchaseOrderDetail");

            entity.Property(e => e.UnitPrice)
                .HasColumnType("NUMERIC");

            entity.HasOne(d => d.InventroyItem)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.InventroyItemId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.PurchaseOrder)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.OrderStatus)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.OrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasData(
                new PurchaseOrderDetail() { PurchaseOrderDetailId = 50001, PurchaseOrderId = 5001, InventroyItemId = 20001, OrderStatusId = 1, Quantity = 3, UnitPrice = 141.95M }
            );
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.StateId)
                .ValueGeneratedNever();

            entity.Property(e => e.SalesTax)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMERIC");

            entity.HasData(
                new State() { StateId = 17, Code = "IL", Name = "Illinois", SalesTax = 0.0882M },
                new State() { StateId = 18, Code = "IN", Name = "Indiana", SalesTax = 0.07M },
                new State() { StateId = 26, Code = "MI", Name = "Michigan", SalesTax = 0.06M },
                new State() { StateId = 39, Code = "OH", Name = "Ohio", SalesTax = 0.0723M },
                new State() { StateId = 55, Code = "WI", Name = "Wisconsin", SalesTax = 0.0543M }
            );
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendor");

            entity.HasOne(d => d.Address)
                .WithMany(p => p.Vendors)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasData(
                new Vendor() { VendorId = 101, AddressId = 10001, Name = "Ox Exports" }
            );
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
