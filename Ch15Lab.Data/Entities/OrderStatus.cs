namespace Ch15Lab.Data.Entities;

public class OrderStatus
{
    public int OrderStatusId { get; set; }
    public string Description { get; set; } = null!;

    public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = null!;
}