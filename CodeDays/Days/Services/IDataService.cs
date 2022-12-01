using Days.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Services
{
    public interface IDataService
    {
        Task<List<T>> GetAllAsync<T>() where T : class, IDataServiceModel, new();

        Task ClearAllAsync<T>() where T : class, IDataServiceModel, new();

        Task UpdateAsync<T>(T entity) where T : class, IDataServiceModel, new();
        Task InsertAsync<T>(T entity) where T : class, IDataServiceModel, new();

        Task<FunName> GetRandomFunNameAsync();

        Task<List<Elf>> GetAllElvesAndFoodAsync();
        Task ClearAllElvesAndFoodAsync();
    }
}
