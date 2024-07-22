using DataAccess.Models;


namespace DataAccess.Data.IDataModel
{
    public interface IRefundOrderData
    {
        Task<int> RefundOrder(Order<Item> Order);
       
        Task<IEnumerable<Order<OrderDetail>>> GetOrderByTransNo(int LocationID, string TransNo);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByTransNo(int LocationID, string TransNo);
        Task<IEnumerable<OrderModifierDetail>> GetOrderTransNoModifiers(int LocationID, string TransNo);
    }
}