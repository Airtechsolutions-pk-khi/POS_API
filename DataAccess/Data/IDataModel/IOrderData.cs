﻿using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface IOrderData
    {
        Task<OrderReturn> SaveData(Order<Item> Order);
        Task UpdateData(Order<Item> Order);
        Task<IEnumerable<Order<OrderDetail>>> GetOrderByLocation(int LocationID, DateTime FromDate, DateTime ToDate);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByLocation(int LocationID, DateTime FromDate, DateTime ToDate);
	}
}