using Days.Helpers;
using Days.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Services
{
    public class DataService : IDataService
    {
        private const string DB_NAME = "advent_of_code.db3";
        private readonly string _dbPath;
        private SQLiteAsyncConnection ConnAsnyc { get; set; } 

        public DataService()
        {
            _dbPath = FileAccessHelper.GetLocalFilePath(DB_NAME);
            InitialiseDataBase();
            ConnAsnyc = new SQLiteAsyncConnection(_dbPath);
        }

        public string StatusMessage { get; private set; }

        public async Task<List<T>> GetAllAsync<T>() where T : class, IDataServiceModel, new()
        {
            try
            {
                return await ConnAsnyc.Table<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data of type '{nameof(T)}'. {ex.Message}";
            }
            return null;
        }

        public async Task ClearAllAsync<T>() where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.Table<T>().DeleteAsync(e => e.Id > 0);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data of type '{nameof(T)}'. {ex.Message}";
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update data of type '{nameof(T)}'. {ex.Message}";
            }
        }
        public async Task InsertAsync<T>(T entity) where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to insert data of type '{nameof(T)}'. {ex.Message}";
            }
        }

        public async Task<FunName> GetRandomFunNameAsync()
        {
            try
            {
                var names = await ConnAsnyc.Table<FunName>().ToListAsync();
                return names.OrderBy(e => Guid.NewGuid()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }
            return null;
        }

        public async Task<List<Elf>> GetAllElvesAndFoodAsync()
        {
            try
            {
                var elves = await ConnAsnyc.Table<Elf>().ToListAsync();
                foreach (var elf in elves)
                {
                    elf.Foods = await ConnAsnyc.Table<Food>().Where(f => f.ElfId == elf.Id).ToListAsync();
                }
                return elves;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to retrieve data. {ex.Message}";
            }
            return null;
        }

        public async Task ClearAllElvesAndFoodAsync()
        {
            var elves = await ConnAsnyc.Table<Elf>().ToListAsync();
            foreach (var elf in elves)
            {
                await ConnAsnyc.Table<Food>().DeleteAsync(f => f.ElfId == elf.Id);
            }
            await ClearAllAsync<Elf>();
        }
        private void InitialiseDataBase()
        {
            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.CreateTable<Elf>();
                conn.CreateTable<Food>();
                conn.CreateTable<FunName>();
                conn.CreateTable<RockPaperScissorsPlay>();

                SeedFunName(conn);
            }

        }

        private void SeedFunName(SQLiteConnection conn)
        {
            if (conn.Table<FunName>().Count() > 0)
            {
                return;
            }
            conn.Table<FunName>().Delete(e => e.Id > 0);
            conn.Insert(new FunName { Name = "Valerio" });
            conn.Insert(new FunName { Name = "Brian" });
            conn.Insert(new FunName { Name = "Bryan" });
            conn.Insert(new FunName { Name = "Carl" });
            conn.Insert(new FunName { Name = "Fabrizio" });
            conn.Insert(new FunName { Name = "James" });
            conn.Insert(new FunName { Name = "Vikas" });
            conn.Insert(new FunName { Name = "Scott" });
            conn.Insert(new FunName { Name = "Conor" });
            conn.Insert(new FunName { Name = "Urszula" });
            conn.Insert(new FunName { Name = "Shaun" });
            conn.Insert(new FunName { Name = "Colm" });
            conn.Insert(new FunName { Name = "Shane" });
            conn.Insert(new FunName { Name = "Robert" });
            conn.Insert(new FunName { Name = "Divan" });
            conn.Insert(new FunName { Name = "Dermot" });
            conn.Insert(new FunName { Name = "Athira" });
            conn.Insert(new FunName { Name = "Ekaterina" });
            conn.Insert(new FunName { Name = "Ian" });
            conn.Insert(new FunName { Name = "Stephen" });
            conn.Insert(new FunName { Name = "Christopher" });
            conn.Insert(new FunName { Name = "Alan" });
            conn.Insert(new FunName { Name = "Eamonn" });
            conn.Insert(new FunName { Name = "Gavin" });
            conn.Insert(new FunName { Name = "Kieran" });
            conn.Insert(new FunName { Name = "Jeffrey" });
            conn.Insert(new FunName { Name = "Adrian" });
            conn.Insert(new FunName { Name = "Hazel" });
            conn.Insert(new FunName { Name = "Cameron" });
            conn.Insert(new FunName { Name = "Ashley" });
            conn.Insert(new FunName { Name = "Sharad" });
            conn.Insert(new FunName { Name = "Merlin Mary" });
            conn.Insert(new FunName { Name = "Lorna" });
            conn.Insert(new FunName { Name = "Dennis" });
            conn.Insert(new FunName { Name = "Drew" });
            conn.Insert(new FunName { Name = "David" });
            conn.Insert(new FunName { Name = "Parun" });
            conn.Insert(new FunName { Name = "Joe" });
            conn.Insert(new FunName { Name = "Inesh" });
            conn.Insert(new FunName { Name = "Arthur" });
            conn.Insert(new FunName { Name = "Samira" });
            conn.Insert(new FunName { Name = "Agnieszka" });
            conn.Insert(new FunName { Name = "Lixon" });
            conn.Insert(new FunName { Name = "Michal" });
            conn.Insert(new FunName { Name = "Gary" });
            conn.Insert(new FunName { Name = "Carne" });
            conn.Insert(new FunName { Name = "Rene" });
            conn.Insert(new FunName { Name = "John" });
            conn.Insert(new FunName { Name = "Cody" });
            conn.Insert(new FunName { Name = "Terry" });
            conn.Insert(new FunName { Name = "Irina" });
        }
    }
}
