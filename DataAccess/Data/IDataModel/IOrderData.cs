using DataAccess.Models;


namespace DataAccess.Data.IDataModel
{
    public interface IOrderData 
    {
        Task<OrderReturn> SaveData(Order<Item> Order);
        Task<OrderReturn> SaveCreditData(Order<Item> Order);
        Task UpdateData(Order<Item> Order);
        Task<IEnumerable<Order<OrderDetail>>> GetOrderByLocation(int LocationID, DateTime FromDate, DateTime ToDate);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByLocation(int LocationID, DateTime FromDate, DateTime ToDate);
		Task<IEnumerable<OrderModifierDetail>> GetOrderModifiers(int LocationID, DateTime FromDate, DateTime ToDate);

        Task<IEnumerable<Order<OrderDetail>>> GetOrderByID(int LocationID, int OrderID);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByID(int LocationID, int OrderID);
        Task<IEnumerable<OrderModifierDetail>> GetOrderIDModifiers(int LocationID, int OrderID);
    }
}