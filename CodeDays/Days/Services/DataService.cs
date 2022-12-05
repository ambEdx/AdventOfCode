using CommunityToolkit.Mvvm.Messaging;
using Days.Helpers;
using Days.Messages;
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

        public async Task<List<T>> GetAllAsync<T>() where T : class, IDataServiceModel, new()
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Retrieving data of type '{typeof(T).Name}' ..."));
                return await ConnAsnyc.Table<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to retrieve data of type '{typeof(T).Name}'. {ex.Message}"));
            }
            return null;
        }

        public async Task ClearAllAsync<T>() where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.Table<T>().DeleteAsync(e => e.Id > 0);
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Cleared data of type '{typeof(T).Name}'."));
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to clear data of type '{typeof(T).Name}'. {ex.Message}"));
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.UpdateAsync(entity);
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Updated data of type '{typeof(T).Name}'."));
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to update data of type '{typeof(T).Name}'. {ex.Message}"));
            }
        }
        public async Task InsertAsync<T>(T entity) where T : class, IDataServiceModel, new()
        {
            try
            {
                await ConnAsnyc.InsertAsync(entity);
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Saved data of type '{typeof(T).Name}'."));
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to insert data of type '{typeof(T).Name}'. {ex.Message}"));
            }
        }

        public async Task<string> GetInputOfDayAsync(int day)
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Retrieving raw input for day {day} ..."));
                var rawInput = await ConnAsnyc.Table<RawInput>().FirstOrDefaultAsync(e => e.Day == day);
                return rawInput?.Input;
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to retrieve raw input for day {day}. {ex.Message}"));
            }
            return null;
        }
        public async Task ClearInputOfDayAsync(int day)
        {
            try
            {
                await ConnAsnyc.Table<RawInput>().DeleteAsync(e => e.Day == day);
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to clear raw input for day {day}. {ex.Message}"));
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
                WeakReferenceMessenger.Default.Send(new DataServiceMessage($"Failed to get a random name. {ex.Message}"));
            }
            return null;
        }

        private void InitialiseDataBase()
        {
            using (var conn = new SQLiteConnection(_dbPath))
            {
                conn.CreateTable<FunName>();
                conn.CreateTable<RawInput>();

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
