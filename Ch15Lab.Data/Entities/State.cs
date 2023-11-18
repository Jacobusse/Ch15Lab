using System;
using System.Collections.Generic;

namespace Ch15Lab.Data.Entities;

public partial class State
{
    public int StateId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal SalesTax { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
