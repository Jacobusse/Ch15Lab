using Ch15Lab.Data.Contexts;
using Ch15Lab.Data.Entities;
using Ch15Lab.Data.Repos;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Ch15Lab.Test;

public class PurchaseOrderRepoUnitTest
{
    private readonly POManagerContext _db;

    public PurchaseOrderRepoUnitTest()
    {
        using var connection = new SqliteConnection("DataSource=file::memory:?cache-shared");
        var opt = new DbContextOptionsBuilder<POManagerContext>()
            .UseSqlite(connection)
            .Options;

        _db = new POManagerContext(opt);
        _db.Database.EnsureCreated();
    }

    [Fact]
    public void CheckDatabase_EnsureCreated()
    {
        Assert.True(!_db.Database.EnsureCreated());
    }

    [Fact]
    public void GetPO_Successful()
    {
        PurchaseOrderRepo repoPO = new PurchaseOrderRepo(NullLogger<PurchaseOrderRepo>.Instance, _db);

        PurchaseOrder? po = repoPO.Get(5001); // Initialy Generated
        if (po == null)
            Assert.Fail("PO is null");

        Assert.Equal("NN6003", po.Number);
        Assert.Equal(1, po.PurchaseOrderDetails?.Count() ?? 0);
        Assert.Equal("Ox Imports", po.Customer?.Name ?? "");
    }

    [Fact]
    public void AddPO_Successful()
    {
        PurchaseOrderRepo repoPO = new PurchaseOrderRepo(NullLogger<PurchaseOrderRepo>.Instance, _db);

        // New PO must have a few things...
        PurchaseOrder po = new PurchaseOrder() 
        {
            PurchaseOrderId = 0,
            Number = "987",
            Date = new DateOnly(2023, 2, 12),
            Note = "Test Note",
            SalesTax = 0.06M,
            CustomerId = 1001, // Customer 
            BillingAddress = new Address() 
            {
                AddressId = 0, // Add Address
                Title = "Billing Test",
                Street = "1234 Hard Rd",
                City = "Holland",
                StateId = 26, // Michigan
                PostalCode = "49424"
            },
            ShippingAddress = new Address() 
            {
                AddressId = 0, // Add Address
                Title = "Shipping Test",
                Street = "98 Easy St",
                City = "Grand Rapids",
                StateId = 26, // Michigan
                PostalCode = "49501"
            },
            PurchaseOrderDetails = new List<PurchaseOrderDetail>() 
            {
                new PurchaseOrderDetail() 
                {
                    PurchaseOrderDetailId = 0,
                    OrderStatusId = 1, // Ordered
                    InventroyItemId = 20002, // 8" Vase
                    Quantity = 4,
                    UnitPrice = 35.97M
                },
                new PurchaseOrderDetail() 
                {
                    PurchaseOrderDetailId = 0,
                    OrderStatusId = 1, // Ordered
                    InventroyItemId = 20003, // 6 Sided Vase
                    Quantity = 3,
                    UnitPrice = 51.57M
                }
            }
        };

        var poNew = repoPO.Add(po);

        Assert.NotNull(poNew);
        Assert.NotEqual(0, poNew?.PurchaseOrderId);
        Assert.Equal("987", poNew?.Number);
    }

    [Fact]
    public void SetPO_Successful()
    {
        PurchaseOrderRepo repoPO = new PurchaseOrderRepo(NullLogger<PurchaseOrderRepo>.Instance, _db);

        PurchaseOrder? po = repoPO.Get(5001); // Initialy Generated
        if (po == null)
            Assert.Fail("PO is null");

        string holdTitle = new string(po.ShippingAddress.Title);

        po.Date = new DateOnly(2023, 5, 5);
        po.ShippingAddress.Title += " Edited"; // will not update

        PurchaseOrder? poNew = repoPO.Set(po);

        Assert.NotNull(poNew);
        Assert.Equal("NN6003", poNew.Number);
        Assert.NotEqual(holdTitle + " Edited", poNew.ShippingAddress?.Title);
        Assert.Equal(1, po.PurchaseOrderDetails?.Count());
        Assert.Equal("Ox Imports", po.Customer?.Name);
    }

    [Fact]
    public void DelPO_Successful()
    {
        PurchaseOrderRepo repoPO = new PurchaseOrderRepo(NullLogger<PurchaseOrderRepo>.Instance, _db);
        
        Assert.Throws<NotImplementedException>(() => repoPO.Del(5001));
    }
}
