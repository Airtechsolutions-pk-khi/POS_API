namespace DataAccess.Services.IService
{
    public interface IGenericCrudService
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);        
        Task SaveData<T>(string SP, T parameters);
        //Task InsertModifier<T>(string SP, T parameters);
        Task<S> SaveSingleQueryable<S, T>(string SP, T parameters);
		Task<T> LoadSingleOrDefaultData<T, U>(string SP, U parameters);
        Task<Tuple<IEnumerable<T1>, IEnumerable<T2>>> LoadMultipleData<T1, T2, TParams>(string storedProcedure, TParams parameters);

    }
}
