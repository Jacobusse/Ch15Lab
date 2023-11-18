using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public string? Number { get; set; }

    public DateOnly Date { get; set; }

    public int BillingAddressId { get; set; }

    public int ShippingAddressId { get; set; }

    public int CustomerId { get; set; }

    public string? Note { get; set; }

    public decimal SalesTax { get; set; }

    public virtual Address BillingAddress { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = new List<PurchaseOrderDetail>();

    public virtual Address ShippingAddress { get; set; } = null!;
}
