using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class PurchaseOrderDetail
{
    public int PurchaseOrderDetailId { get; set; }

    public int PurchaseOrderId { get; set; }

    public int OrderStatusId { get; set; }

    public int InventroyItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual OrderStatus OrderStatus {get;set;} = null!;

    public virtual InventoryItem InventroyItem { get; set; } = null!;

    public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
}
