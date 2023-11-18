using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class InventoryItem
{
    public int InventoryItemId { get; set; }

    public string SkuNumber { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int VendorId { get; set; }

    public decimal Cost { get; set; }

    public decimal RetailPrice { get; set; }

    public int Count { get; set; }

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Vendor Vendor { get; set; } = null!;
}
