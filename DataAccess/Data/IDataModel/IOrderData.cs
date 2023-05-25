using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IOrderData
    {
        Task SaveData(Order Order);
        Task UpdateData(Order Order);
        Task<IEnumerable<Order>> GetOrderByLocation(int CustomerID);
        Task<IEnumerable<OrderItem>> GetOrderDetailsByLocation(int CustomerID);
	}
}