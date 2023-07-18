namespace DataAccess.Services.IService
{
    public interface IGenericCrudService
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);
        Task SaveData<T>(string SP, T parameters);
		Task<S> SaveSingleQueryable<S, T>(string SP, T parameters);
		Task<T> LoadSingleOrDefaultData<T, U>(string SP, U parameters);
	}
}
