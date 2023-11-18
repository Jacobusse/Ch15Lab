using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class Vendor
{
    public int VendorId { get; set; }

    public string Name { get; set; } = null!;

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = null!;
}
