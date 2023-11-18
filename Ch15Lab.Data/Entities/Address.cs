using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class Address
{
    public int AddressId { get; set; }

    public int AddressTypeId { get; set; }

    public string Title { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public int StateId { get; set; }

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderBillingAddresses { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<PurchaseOrder> PurchaseOrderShippingAddresses { get; set; } = new List<PurchaseOrder>();

    public virtual State State { get; set; } = null!;

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
