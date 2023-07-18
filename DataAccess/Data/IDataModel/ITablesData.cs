using DataAccess.Models;

namespace DataAccess.Data.IDataModel
{
    public interface ITablesData
    {
        Task<IEnumerable<Table>> GetAllTables(int LocationID);
    }
}