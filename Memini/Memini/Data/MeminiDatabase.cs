using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Memini.Models;
using SQLiteNetExtensionsAsync.Extensions;

namespace Memini.Data
{
    public class MeminiDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public MeminiDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Theme>().Wait();
            _database.CreateTableAsync<Word>().Wait();
        }

        public Task<List<Theme>> GetThemesAsync()
        {
           return _database.GetAllWithChildrenAsync<Theme>();
        }

        public Task<Theme> GetThemeAsync(int id)
        {
            return _database.GetWithChildrenAsync<Theme>(id);
        }

        public Task SaveThemeAsync(Theme theme)
        {
            if (theme.ID != 0)
            {
                return _database.UpdateWithChildrenAsync(theme);
            }
            else
            {
                return _database.InsertAsync(theme);
            }
        }

        public Task<int> SaveWordAsync(Word word)
        {
            if (word.ID != 0)
            {
                return _database.UpdateAsync(word);
            }
            else
            {
                return _database.InsertAsync(word);
            }
        }

        public Task DeleteThemeAsync(Theme theme)
        {
            _database.DeleteAllAsync(theme.Words);
            return _database.DeleteAsync(theme);
        }
        public Task DeleteWordAsync(Word word)
        {
            return _database.DeleteAsync(word);
        }
    }
}
