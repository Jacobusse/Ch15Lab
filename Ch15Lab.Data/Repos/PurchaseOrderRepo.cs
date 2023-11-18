using Ch15Lab.Data.Contexts;
using Ch15Lab.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ch15Lab.Data.Repos;

public class PurchaseOrderRepo : IRepo<PurchaseOrder>
{
    private readonly ILogger<PurchaseOrderRepo> _logger;
    private POManagerContext _db;

    public PurchaseOrderRepo(ILogger<PurchaseOrderRepo> logger, POManagerContext db)
    {
        _logger = logger;
        _db = db;
    }

    public PurchaseOrder? Add(PurchaseOrder entity)
    {
        try
        {
            var insert = _db.PurchaseOrders.Add(entity);
            _db.SaveChanges();

            return insert?.Entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Add Purchase Order {entity.Number} Created Exception");
        }

        return null;
    }

    public void Del(int id)
    {
        throw new NotImplementedException();
    }

    public PurchaseOrder? Get(int id)
    {
        try
        {
            return _db.PurchaseOrders
                .Include(po => po.BillingAddress)
                .Include(po => po.ShippingAddress)
                .Include(po => po.PurchaseOrderDetails)
                .Include(po => po.Customer)
                .SingleOrDefault(po => po.PurchaseOrderId == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Get Purchase Order {id} Created Exception");
        }

        return null;
    }

    public PurchaseOrder? Set(PurchaseOrder entity)
    {
        try
        {
            var update = _db.PurchaseOrders.Update(entity);
            _db.SaveChanges();

            return update?.Entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Set Purchase Order {entity.Number} Created Exception");
        }

        return null;
    }
}

