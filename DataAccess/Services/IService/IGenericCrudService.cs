using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services.IService
{
    public interface IGenericCrudService
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);

        Task SaveData<T>(string SP, T parameters);
    }
}
