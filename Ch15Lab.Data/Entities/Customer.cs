using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId {get;set;}

    public virtual Address Address {get;set;} = null!;

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
